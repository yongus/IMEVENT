using IMEVENT.Data;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IMEVENT.Services
{
    public class ExcelDataExtractor : IDataExtractor
    {

        public ApplicationDbContext DBcontext { get; set; }
        public string Source { get; set; }
        // for now we are going to stop the processing of the file if we see 5 consecutive empty lines.
        private static int MAX_EMPTY_CELLS = 5;
        private static readonly string COLUMN_LASTNAME = "B";
        private static readonly string COLUMN_FIRSTNAME = "C";
        private static readonly string COLUMN_SEX = "D";
        private static readonly string COLUMN_RETRAITE = "E";
        private static readonly string COLUMN_TOWN = "F";
        private static readonly string COLUMN_GROUP = "G";
        private static readonly string COLUMN_LEVEL = "H";
        private static readonly string COLUMN_INVITED_BY = "I";
        private static readonly string COLUMN_SHARING_CATEGORY = "J";
        private static readonly string COLUMN_LANGUAGE = "K";
        private static readonly string COLUMN_AMOUNTPAID = "L";
        private static readonly string COLUMN_EMAIL = "M";
        private static readonly string COLUMN_PHONE = "N";
        private static readonly string COLUMN_RESPONSIBLE= "O";
        private static readonly string COLUMN_REMARKS = "P";
        private static readonly string COLUMN_REGIME = "Q";
        private static readonly string COLUMN_PRECISION = "R";
        private static readonly string COLUMN_ZONE = "S";
        private static readonly string COLUMN_SOUS_ZONE = "T";
        private static readonly string COLUMN_ORIGIN_TOWN = "U";
        private static readonly string COLUMN_ORIGIN_GROUP = "V";
        private static readonly string COLUMN_HALL = "W";
        private static readonly string COLUMN_REFECTORY = "X";
        private static readonly string COLUMN_DORMITORY = "Y";
        private static readonly string COLUMN_PART = "Z";


        public void ExtractDataFromSource(string source, int IdEvent)
        {
            this.Source = source;
            FileInfo existingFile = new FileInfo(source);
            if (!existingFile.Exists)
            {
                return;
            }

            using(ExcelPackage package = new ExcelPackage(existingFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                int maxEmpty = 0;
                int currentRow = 2;
                while(maxEmpty < MAX_EMPTY_CELLS)
                {
                    EventAttendee attendee = new EventAttendee();
                    string name = (string)worksheet.Cells[COLUMN_FIRSTNAME + Convert.ToString(currentRow)].Value;
                    if (!String.IsNullOrEmpty(name))
                    {
                        User u = getUserFromSpreadSheet(currentRow, worksheet);

                        maxEmpty = 0;
                    }
                    else
                    {
                        maxEmpty++;
                    }
                    currentRow++;
                }

               
            }
        }
        private User getUserFromSpreadSheet(int row, ExcelWorksheet sheet )
        {
            User user = new User();
            user.FirstName = (string)sheet.Cells[COLUMN_FIRSTNAME + Convert.ToString(row)].Value;
            user.LastName = (string)sheet.Cells[COLUMN_LASTNAME + Convert.ToString(row)].Value;
            user.Sex = (string)sheet.Cells[COLUMN_SEX + Convert.ToString(row)].Value;
            user.Level = (string)sheet.Cells[COLUMN_LEVEL + Convert.ToString(row)].Value;
            if(sheet.Cells[COLUMN_PHONE + Convert.ToString(row)].Value != null)
            {
                string val;
                try
                {
                     val = (string)sheet.Cells[COLUMN_PHONE + Convert.ToString(row)].Value;
                }
                catch(InvalidCastException e)
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
           
            bool isResponsible =false;
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
            user.IdZone = zone.Id;

            SousZone sousZone = new SousZone();
            sousZone.Label = (string)sheet.Cells[COLUMN_SOUS_ZONE + Convert.ToString(row)].Value;
            sousZone.IdSousZone = SousZone.GetIdRefectoryIdByLabel(DBcontext, zone.Label);
            sousZone.IdZone = zone.Id;
            sousZone.IdSousZone = zone.persist();
            user.IdSousZone = sousZone.IdSousZone;

            Group group = new Group();
            group.IdZone = zone.Id;
            group.IdSousZone = sousZone.IdSousZone;
            group.Label = (string)sheet.Cells[COLUMN_GROUP + Convert.ToString(row)].Value;
            group.Id = Group.GetIdGroupIdByLabel(DBcontext, group.Label);
            group.Id = group.persist();
            group.IdSousZone = sousZone.IdSousZone;
            group.IdZone = zone.Id;
            user.Id = user.persist();
            return user;
        }
        
    }
}
