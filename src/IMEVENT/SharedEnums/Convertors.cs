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
        
        public static string DormitoryTypeToString(this DormitoryTypeEnum type)
        {
            switch (type)
            {                
                case DormitoryTypeEnum.MALE:
                    return "Dortoir Hommes";
                case DormitoryTypeEnum.FEMALE:
                    return "Dortoir Femmes";
                case DormitoryTypeEnum.YOUNGBOYS:
                    return "Dortoir Jeunes Garcons";
                case DormitoryTypeEnum.YOUNGGIRLS:
                    return "Dortoir Jeunes Filles";
                case DormitoryTypeEnum.NONE:                    
                default:
                    return "Inconnu";
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
    }
}
