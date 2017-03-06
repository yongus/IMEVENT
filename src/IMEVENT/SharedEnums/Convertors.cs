using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMEVENT.SharedEnums
{
    public static class Convertors
    {
        public static string MemberShipLevelToString(this MembershipLevelEnum level)
        {
            switch (level)
            {                
                case MembershipLevelEnum.SIMPLE:
                    return "Simple";
                case MembershipLevelEnum.REGULIER:
                    return "Regulier";
                case MembershipLevelEnum.ACTIF_1:
                    return "Actif I";
                case MembershipLevelEnum.ACTIF_2:
                    return "Actif II";
                case MembershipLevelEnum.ACTIF_3:
                    return "Actif III";
                case MembershipLevelEnum.JEUNE_PHARE:
                    return "Jeune Phare";
                case MembershipLevelEnum.ACCOMPAGNATEUR:
                    return "Accompagnateur";
                case MembershipLevelEnum.AEF:
                    return "AEF";
                case MembershipLevelEnum.CMP:
                    return "Candidat Membre Plein";
                case MembershipLevelEnum.MP:
                    return "Membre Plein";
                case MembershipLevelEnum.INCARNATEUR:
                    return "Incarnateur";
                case MembershipLevelEnum.RG:
                    return "Responsable General";
                case MembershipLevelEnum.INVITE:
                    return "Invited";
                default:
                    return "Inconnu";
            }
        }

        public static MembershipLevelEnum GetMembershipLevel( string text)
        {
            text = text.ToLower().Trim();
            switch (text)
            {
                case "simple":
                    return MembershipLevelEnum.SIMPLE;
                case "regulier":
                    return MembershipLevelEnum.REGULIER;
                case "actifI":
                    return MembershipLevelEnum.ACTIF_1;
                case "actifII":
                    return MembershipLevelEnum.ACTIF_2;
                case "actifIII":
                    return MembershipLevelEnum.ACTIF_3;
                case "jeunephare":
                    return MembershipLevelEnum.JEUNE_PHARE;
                case "accompagnateur":
                    return MembershipLevelEnum.ACCOMPAGNATEUR;
                case "aef":
                    return MembershipLevelEnum.AEF;
                case "candidatmembreplein":
                    return MembershipLevelEnum.CMP;
                case "membreplein":
                    return MembershipLevelEnum.MP;
                case "incarnateur":
                    return MembershipLevelEnum.INCARNATEUR;
                case "responsablegeneral":
                    return MembershipLevelEnum.RG;               
                default:
                    return MembershipLevelEnum.INVITE;
            }
        }

        public static string DormitoryTypeToString(this DormitoryTypeEnum type)
        {
            string ret = "Dortoir ";
            switch (type)
            {                
                case DormitoryTypeEnum.MATELAS:
                    return ret + "Matelas";
                case DormitoryTypeEnum.BED:
                    return ret + "Lits";
                case DormitoryTypeEnum.VIP:
                    return ret + "VIP";                
                default:
                    return "Inconnu";
            }
        }

        public static DormitoryTypeEnum GetDormirtoryType(string type)
        {
            type = type.ToLower().Trim();
            switch (type)
            {
                case "male":
                    return DormitoryTypeEnum.MALE;
                case "female":
                    return DormitoryTypeEnum.FEMALE;
                case "youngboys":
                    return DormitoryTypeEnum.YOUNGBOYS;
                case "younggirls":
                    return DormitoryTypeEnum.YOUNGGIRLS;
                default:
                    return DormitoryTypeEnum.NONE;
            }

        }
        public static string SharingGroupCategoryToString(this SharingGroupCategoryEnum type)
        {
            switch (type)
            {
                case SharingGroupCategoryEnum.ADULTE_S:
                    return "Adulte Senior";
                case SharingGroupCategoryEnum.ADULTE_M:
                    return "Adulte Majeur";
                case SharingGroupCategoryEnum.UNIVERSITAIRE_DEBUTANT:
                    return "Universitaire Debutant";
                case SharingGroupCategoryEnum.UNIVERSITAIRE_MAJEUR:
                    return "Universitaire Majeur";
                case SharingGroupCategoryEnum.JEUNE_MARIE:
                    return "Jeune Marie";
                case SharingGroupCategoryEnum.JEUNE_TRAVAILLEUR_MAJEUR:
                    return "Jeune Travailleur Majeur";
                case SharingGroupCategoryEnum.JEUNE_TRAVAILLEUR:
                    return "Jeune Travailleur";
                case SharingGroupCategoryEnum.JEUNE_TRAVAILLEUR_SENIOR:
                    return "Jeune Travailleur Senior";
                case SharingGroupCategoryEnum.SECOND_INTERMEDIARE:
                    return "Secondaire Intermediaire";
                case SharingGroupCategoryEnum.SECOND_JUNIOR:
                    return "Secondaire Junior";
                default:
                    return "Inconnu";
            }
        }        

        public static SharingGroupCategoryEnum GetSharingGroupCategory(string type)
        {
            type = type.ToLower().Trim();
            switch (type)
            {
                case "adultesenior":
                    return SharingGroupCategoryEnum.ADULTE_S;
                case "adultemajeur":
                    return SharingGroupCategoryEnum.ADULTE_M;
                case "universitairedebutant":
                    return SharingGroupCategoryEnum.UNIVERSITAIRE_DEBUTANT;
                case "universitairemajeur":
                    return SharingGroupCategoryEnum.UNIVERSITAIRE_MAJEUR;
                case "jeunemarie":
                    return SharingGroupCategoryEnum.JEUNE_MARIE;
                case "jeunetravailleurmajeur":
                    return SharingGroupCategoryEnum.JEUNE_TRAVAILLEUR_MAJEUR;
                case "jeunetravailleur":
                    return SharingGroupCategoryEnum.JEUNE_TRAVAILLEUR;
                case "jeunetravailleursenior":
                    return SharingGroupCategoryEnum.JEUNE_TRAVAILLEUR_SENIOR;
                case "secondaireintermediaire":
                    return SharingGroupCategoryEnum.SECOND_INTERMEDIARE;
                case "secondairejunior":
                    return SharingGroupCategoryEnum.SECOND_JUNIOR;
                default:
                    return SharingGroupCategoryEnum.UNIVERSITAIRE_DEBUTANT;

            }
        }

        public static string RegimeToString(this RegimeEnum type)
        {
            switch (type)
            {
                case RegimeEnum.YES:
                    return "Oui";
                case RegimeEnum.NO:
                    return "Non";
                case RegimeEnum.AGE:
                    return "Age";
                case RegimeEnum.NONE:
                    return "RAS";
                case RegimeEnum.OTHER:
                default:
                    return "Inconnu";
            }
        }

        public static RegimeEnum GetRegimeType(string type)
        {
            type = type.ToLower().Trim();
            switch (type)
            {
                case "oui":
                    return RegimeEnum.YES;
                case "non":
                    return RegimeEnum.NO;
                case "age":
                    return RegimeEnum.AGE;
                case "ras":
                    return RegimeEnum.NONE;
                default:
                    return RegimeEnum.NONE;
                 
            }

        }
        public static string HallSectionTypeToString(HallSectionTypeEnum type)
        {
            switch (type)
            {
                case HallSectionTypeEnum.NONE:
                    return "Aucune";
                case HallSectionTypeEnum.RESPONSABLE_GENERAL:
                    return "Responsable General";
                case HallSectionTypeEnum.SERVICE_CHANT:
                    return "Service Chant";
                case HallSectionTypeEnum.SERVICE_INSTRUMENT:
                    return "Service Instruments";
                case HallSectionTypeEnum.SERVICE_TRADUCTION:
                    return "Service Traduction";
                case HallSectionTypeEnum.SPECIAL_GUEST:
                    return "Invite special";
                default:
                    return "Inconnu";

            }
        }

        public static HallSectionTypeEnum GetHallSectionType(string type)
        {
            type = type.ToLower().Trim();
            switch (type)
            {
                case "aucune":
                    return HallSectionTypeEnum.NONE;
                case "responsablegeneral":
                    return HallSectionTypeEnum.RESPONSABLE_GENERAL;
                case "servicechant":
                    return HallSectionTypeEnum.SERVICE_CHANT;
                case "serviceinstruments":
                    return HallSectionTypeEnum.SERVICE_INSTRUMENT;
                case "servicetraduction":
                    return HallSectionTypeEnum.SERVICE_TRADUCTION;
                case "invitespecial":
                    return HallSectionTypeEnum.SPECIAL_GUEST;
                default:
                    return HallSectionTypeEnum.NONE;
            }

        }

       
       
    }


    
    
}
