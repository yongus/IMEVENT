using IMEVENT.Data;
using IMEVENT.SharedEnums;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NLog;

namespace IMEVENT.Services
{
    public partial class ExcelDataExtractor : IDataExtractor
    {
        private static Logger log = LogManager.GetCurrentClassLogger();


        public ApplicationDbContext DBcontext { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        // for now we are going to stop the processing of the file if we see 5 consecutive empty lines.
        private static int MAX_EMPTY_CELLS = 5;
        private static readonly string COLUMN_LASTNAME = "B";
        private static readonly string COLUMN_FIRSTNAME = "C";
        private static readonly string COLUMN_SEX = "D";
        private static readonly string COLUMN_RETREAT = "E";
        private static readonly string COLUMN_TOWN = "F";
        private static readonly string COLUMN_GROUP = "G";
        private static readonly string COLUMN_LEVEL = "H";
        private static readonly string COLUMN_INVITED_BY = "I";
        private static readonly string COLUMN_SHARING_CATEGORY = "J";
        private static readonly string COLUMN_LANGUAGE = "K";
        private static readonly string COLUMN_AMOUNTPAID = "L";
        private static readonly string COLUMN_EMAIL = "M";
        private static readonly string COLUMN_PHONE = "N";
        private static readonly string COLUMN_RESPONSIBLE = "O";
        private static readonly string COLUMN_REMARKS = "P";
        private static readonly string COLUMN_REGIME = "Q";
        private static readonly string COLUMN_PRECISION = "R";
        private static readonly string COLUMN_ZONE = "S";
        private static readonly string COLUMN_SOUS_ZONE = "T";
        private static readonly string COLUMN_ORIGIN_TOWN = "U";
        private static readonly string COLUMN_ORIGIN_GROUP = "V";
        private static readonly string COLUMN_HALL_TYPE = "W";
        private static readonly string COLUMN_TABLE_TYPE = "X";
        private static readonly string COLUMN_DORM_TYPE = "Y";
        private static readonly string COLUMN_PART = "Z";

        private static readonly int USER_WORKSHEET_INDEX = 1;
        private static readonly int REF_WORKSHEET_INDEX = 2;
        private static readonly int HALL_WORKSHEET_INDEX = 3;
        private static readonly int DORMS_WORKSHEET_INDEX = 4;

        private static readonly string COLUMN_NAME = "A";
        private static readonly string COLUMN_CAPACITE = "B";

        private static readonly string REFECTORY_NAME = "A";
        private static readonly string TABLE_NAME = "B";
        private static readonly string TABLE_CAPACITY = "C";
        private static readonly string TYPE_TABLE = "D";
        private static readonly string TYPE_DORM = "C";
        private static readonly string TYPE_HALL = "C";

       



        public void ExtractDataFromSource(string source, int EventId)
        {
            this.Source = source;
            FileInfo existingFile = new FileInfo(source);
            if (!existingFile.Exists)
            {
                return;
            }

            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                ExcelWorksheet userWorksheet = package.Workbook.Worksheets[USER_WORKSHEET_INDEX];
                loadUsers(userWorksheet, EventId);
                ExcelWorksheet dormWorksheet = package.Workbook.Worksheets[DORMS_WORKSHEET_INDEX];
                loadDorms(dormWorksheet, EventId);
                ExcelWorksheet hallWorksheet = package.Workbook.Worksheets[HALL_WORKSHEET_INDEX];
                loadHalls(hallWorksheet, EventId);
                ExcelWorksheet refectoryWorksheet = package.Workbook.Worksheets[REF_WORKSHEET_INDEX];
                loadRefectories(refectoryWorksheet, EventId);
            }
        }

        public void loadUsers(ExcelWorksheet worksheet, int EventId)
        {
            int maxEmpty = 0;
            int currentRow = 2;
            while (maxEmpty < MAX_EMPTY_CELLS)
            {
                EventAttendee attendee = new EventAttendee();
                string name = (string)worksheet.Cells[COLUMN_FIRSTNAME + Convert.ToString(currentRow)].Value;
                if (!String.IsNullOrEmpty(name))
                {
                    User u = getUserFromSpreadSheet(currentRow, worksheet);
                    attendee.EventId = EventId;
                    attendee.UserId = u.Id;
                    try
                    {
                        attendee.TableType = Convertors.GetRegimeType((string)worksheet.Cells[COLUMN_TABLE_TYPE + Convert.ToString(currentRow)].Value);
                    }
                    catch
                    {
                        log.Info("Regime import for table failed, setting regime to none");
                        attendee.TableType = RegimeEnum.NONE;
                    }
                    try
                    {
                        attendee.SectionType = Convertors.GetHallSectionType((string)worksheet.Cells[COLUMN_HALL_TYPE + Convert.ToString(currentRow)].Value);
                    }
                    catch
                    {
                        attendee.SectionType = HallSectionTypeEnum.NONE;
                    }
                    try
                    {
                        attendee.DormType = Convertors.GetDormirtoryType((string)worksheet.Cells[COLUMN_DORM_TYPE + Convert.ToString(currentRow)].Value);
                    }
                    catch
                    {
                        attendee.SectionType = HallSectionTypeEnum.NONE;
                    }
                    attendee.Remarks = (string)worksheet.Cells[COLUMN_REMARKS + Convert.ToString(currentRow)].Value;
                    try
                    {

                        attendee.AmountPaid = (int)worksheet.Cells[COLUMN_AMOUNTPAID + Convert.ToString(currentRow)].Value;
                    }
                    catch (Exception)
                    {
                        attendee.AmountPaid = 0;
                    }



                    attendee.InvitedBy = User.GetUserIdByName((string)worksheet.Cells[COLUMN_INVITED_BY + Convert.ToString(currentRow)].Value);
                    try
                    {
                        attendee.OnDiet = worksheet.Cells[COLUMN_REGIME + Convert.ToString(currentRow)].Value.Equals("OUI") ? true : false;

                    }
                    catch (Exception)
                    {
                        attendee.OnDiet = false;
                    }
                    try
                    {
                        attendee.SharingCategory = Convertors.GetSharingGroupCategory((string)worksheet.Cells[COLUMN_SHARING_CATEGORY + Convert.ToString(currentRow)].Value);
                    }
                    catch
                    {
                        attendee.SharingCategory = SharingGroupCategoryEnum.JEUNE_TRAVAILLEUR;
                    }

                    attendee.persist();
                    maxEmpty = 0;
                }
                else
                {
                    maxEmpty++;
                }
                currentRow++;
            }
        }

        public void loadHalls(ExcelWorksheet worksheet, int EventId)
        {
            int maxEmpty = 0;
            int currentRow = 2;
            while (maxEmpty < MAX_EMPTY_CELLS)
            {


                string name = (string)worksheet.Cells[COLUMN_NAME + Convert.ToString(currentRow)].Value;
                if (!String.IsNullOrEmpty(name))
                {
                    getHallsFromSpreadSheet(currentRow, worksheet, EventId);

                    maxEmpty = 0;
                }
                else
                {
                    maxEmpty++;
                }
                currentRow++;
            }
        }

        private void getHallsFromSpreadSheet(int row, ExcelWorksheet sheet, int EventId)
        {
            Hall h = new Hall();
            h.Capacity = Convert.ToInt32(sheet.Cells[COLUMN_CAPACITE + Convert.ToString(row)].Value);
            h.Name = (string)sheet.Cells[COLUMN_NAME + Convert.ToString(row)].Value;
            h.EventId = EventId;
            try
            {
                h.HallType = Convertors.GetHallSectionType((string)sheet.Cells[TYPE_HALL + Convert.ToString(row)].Value);
            }
            catch (Exception)
            {
                h.HallType = HallSectionTypeEnum.NONE;
            }
            h.persist();

        }
        public void loadRefectories(ExcelWorksheet worksheet, int EventId)
        {
            int maxEmpty = 0;
            int currentRow = 2;
            while (maxEmpty < MAX_EMPTY_CELLS)
            {

                string name = (string)worksheet.Cells[COLUMN_NAME + Convert.ToString(currentRow)].Value;
                if (!String.IsNullOrEmpty(name))
                {
                    getRefectoryFromSpreadSheet(currentRow, worksheet, EventId);

                    maxEmpty = 0;
                }
                else
                {
                    maxEmpty++;
                }
                currentRow++;
            }
        }
        private void getRefectoryFromSpreadSheet(int row, ExcelWorksheet sheet, int EventId)
        {
            Refectory h = new Refectory();

            h.Name = (string)sheet.Cells[REFECTORY_NAME + Convert.ToString(row)].Value;
            h.EventId = EventId;
            h.persist();
            Table t = new Table();
            t.Name = (string)sheet.Cells[TABLE_NAME + Convert.ToString(row)].Value;
            t.RefertoireId = h.Id;
            try
            {
                t.Capacity = Convert.ToInt32(sheet.Cells[TABLE_CAPACITY + Convert.ToString(row)].Value);
            }
            catch (Exception)
            {
                t.Capacity = 0;
            }
            try
            {

                t.RegimeType = Convertors.GetRegimeType((string)sheet.Cells[TYPE_TABLE + Convert.ToString(row)].Value.ToString().ToLowerInvariant());
                

            }
            catch (Exception)
            {
                t.RegimeType = RegimeEnum.NONE;
            }
            t.persist();


        }

        public void loadDorms(ExcelWorksheet worksheet, int EventId)
        {
            int maxEmpty = 0;
            int currentRow = 2;
            while (maxEmpty < MAX_EMPTY_CELLS)
            {

                string name = (string)worksheet.Cells[COLUMN_NAME + Convert.ToString(currentRow)].Value;
                if (!String.IsNullOrEmpty(name))
                {
                    getDormFromSpreadSheet(currentRow, worksheet, EventId);
                   
                    maxEmpty = 0;
                }
                else
                {
                    maxEmpty++;
                }
                currentRow++;
            }
        }
        private void getDormFromSpreadSheet(int row, ExcelWorksheet sheet, int EventId)
        {
            Dormitory h = new Dormitory();
            h.Capacity = Convert.ToInt32(sheet.Cells[COLUMN_CAPACITE + Convert.ToString(row)].Value);
            h.Name = (string)sheet.Cells[COLUMN_NAME + Convert.ToString(row)].Value;
            h.EventId = EventId;
            try
            {
                h.DormType = Convertors.GetDormirtoryType((string)sheet.Cells[TYPE_DORM + Convert.ToString(row)].Value.ToString().ToLowerInvariant());
            }
            catch (Exception)
            {
                h.DormType = DormitoryTypeEnum.NONE;
            }
            h.persist();
        }
        private User getUserFromSpreadSheet(int row, ExcelWorksheet sheet)
        {
            User user = new User();
            user.FirstName = (string)sheet.Cells[COLUMN_FIRSTNAME + Convert.ToString(row)].Value;
            user.LastName = (string)sheet.Cells[COLUMN_LASTNAME + Convert.ToString(row)].Value;
            user.Sex = (string)sheet.Cells[COLUMN_SEX + Convert.ToString(row)].Value;
            user.Level = Convertors.GetMembershipLevel((string)sheet.Cells[COLUMN_LEVEL + Convert.ToString(row)].Value);
            if (sheet.Cells[COLUMN_PHONE + Convert.ToString(row)].Value != null)
            {
                string val;
                try
                {
                    val = (string)sheet.Cells[COLUMN_PHONE + Convert.ToString(row)].Value;
                }
                catch (InvalidCastException e)
                {
                    try
                    {
                        val = Convert.ToString((string)sheet.Cells[COLUMN_PHONE + Convert.ToString(row)].Value);
                    }
                    catch (Exception)
                    {
                        val = "";
                    }

                }

                user.PhoneNumber = val;
            }

            bool isResponsible = false;
            if (sheet.Cells[COLUMN_RESPONSIBLE + Convert.ToString(row)].Value == null)
            {
                isResponsible = false;
            }
            else
            {
                string strValue = sheet.Cells[COLUMN_RESPONSIBLE + Convert.ToString(row)].Value.ToString().ToLowerInvariant();
                isResponsible = strValue.Equals("oui") ? true : false;
            }
            user.IsGroupResponsible = isResponsible;
            user.Town = (string)sheet.Cells[COLUMN_TOWN + Convert.ToString(row)].Value;
            // read zone if it is not availlable create it,
            Zone zone = new Zone();
            zone.Label = (string)sheet.Cells[COLUMN_ZONE + Convert.ToString(row)].Value;
            zone.Id = Zone.GetIdRefectoryIdByName(DBcontext, zone.Label);
            zone.Id = zone.persist();
            user.ZoneId = zone.Id;

            SousZone sousZone = new SousZone();
            sousZone.Label = (string)sheet.Cells[COLUMN_SOUS_ZONE + Convert.ToString(row)].Value;
            sousZone.ZoneId = zone.Id;
            sousZone.Id = sousZone.persist();
            user.SousZoneId = sousZone.Id;

            Group group = new Group();
            group.ZoneId = zone.Id;
            group.SousZoneId = sousZone.Id;
            group.Label = (string)sheet.Cells[COLUMN_GROUP + Convert.ToString(row)].Value;
            group.Id = group.persist();
            user.GroupId = group.Id;
            user.persist();
            return user;
        }
        
    }
}