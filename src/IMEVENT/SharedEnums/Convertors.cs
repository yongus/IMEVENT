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
                    return "Régulier";
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
                    return "Responsable Général";
                case MembershipLevelEnum.INVITE:
                    return "Invité";
                default:
                    return "Inconnu";
            }
        }

        public static MembershipLevelEnum GetMembershipLevel( string text)
        {
            text = text.Replace(" ", string.Empty).ToLower();
            switch (text)
            {
                case "simple":
                    return MembershipLevelEnum.SIMPLE;
                case "régulier":
                    return MembershipLevelEnum.REGULIER;
                case "actif1":
                    return MembershipLevelEnum.ACTIF_1;
                case "actif2":
                    return MembershipLevelEnum.ACTIF_2;
                case "actif3":
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
                case "responsablegénéral":
                    return MembershipLevelEnum.RG;
                case "invité":
                default:
                    return MembershipLevelEnum.INVITE;
            }
        }

        public static string DormitoryCategoryToString(this DormitoryCategoryEnum type)
        {
            switch (type)
            {                
                case DormitoryCategoryEnum.MATELAS:
                    return "Matelas";
                case DormitoryCategoryEnum.BED:
                    return "Lit";
                case DormitoryCategoryEnum.VIP:
                    return "VIP";                
                default:
                    return "Inconnu";
            }
        }

        public static string DormitoryTypeToString(this DormitoryTypeEnum type)
        {
            string ret = "Dortoir ";
            switch (type)
            {
                case DormitoryTypeEnum.MEN:
                    return ret + "Homme";
                case DormitoryTypeEnum.WOMEN:
                    return ret + "Femme";
                case DormitoryTypeEnum.NONE:
                    return ret + "Commun";
                default:
                    return "Inconnu";
            }
        }

        public static DormitoryCategoryEnum GetDormirtoryCategory(string type)
        {
            type = type.Replace(" ", string.Empty).ToLower();
            switch (type)
            {
                case "vip":
                    return DormitoryCategoryEnum.VIP;
                case "lit":
                    return DormitoryCategoryEnum.BED;
                case "matelas":                                   
                default:
                    return DormitoryCategoryEnum.MATELAS;
            }
        }

        public static DormitoryTypeEnum GetDormirtoryType(string type)
        {
            type = type.Replace(" ", string.Empty).ToLower();
            switch (type)
            {
                case "femme":
                    return DormitoryTypeEnum.WOMEN;
                case "homme":
                    return DormitoryTypeEnum.MEN;
                case "commun":
                default:
                    return DormitoryTypeEnum.NONE;
            }
        }

        public static string SharingGroupCategoryToString(this SharingGroupCategoryEnum type)
        {
            switch (type)
            {                
                case SharingGroupCategoryEnum.UNIVERSITAIRE_DEBUTANT:
                    return "Universitaire Debutant";
                case SharingGroupCategoryEnum.UNIVERSITAIRE_MAJEUR:
                    return "Universitaire Majeur";                
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
                case SharingGroupCategoryEnum.ADULTE_S:                    
                case SharingGroupCategoryEnum.ADULTE_M:                    
                case SharingGroupCategoryEnum.JEUNE_MARIE:                    
                case SharingGroupCategoryEnum.ADULTE:                    
                default:
                    return "Adulte";
            }
        }        

        public static SharingGroupCategoryEnum GetSharingGroupCategory(string type)
        {
            type = type.ToLower().Replace(" ", string.Empty);
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
                case "adulte": 
                default:
                    return SharingGroupCategoryEnum.ADULTE;
            }
        }

        public static string RegimeToString(this RegimeEnum type)
        {
            switch (type)
            {
                case RegimeEnum.CLERICAL:
                    return "Clergé";
                case RegimeEnum.COOKING:
                    return "Cuisine";
                case RegimeEnum.DISABLED:
                    return "Handicapé";
                case RegimeEnum.FULL_MEMBER:
                    return "Membre Plein";
                case RegimeEnum.GENERAL_MANAGER:
                    return "Responsable Général";
                case RegimeEnum.HEALTH_SERVICE:
                    return "Service Santé";
                case RegimeEnum.MUSIC_INSTRUMENT_SERVICE:
                    return "Service Instrument";
                case RegimeEnum.NEW_BORN:
                    return "Nouveaux-né";
                case RegimeEnum.RELIGIOUS:
                    return "Réligieux";
                case RegimeEnum.SECOND_LANGUAGE:
                    return "Seconde Langue";
                case RegimeEnum.SONG_SERVICE:
                    return "Service Chant";
                case RegimeEnum.SPECIAL_GUEST:
                    return "Invite Spécial";
                case RegimeEnum.TRANSLATION_SERVICE:
                    return "Service Traduction";
                case RegimeEnum.WITHOUT_SALT_WITHOUT_OIL:
                    return "Sans Sel - sans Huile";
                case RegimeEnum.NONE:                    
                default:
                    return "Sans Régime";
            }
        }

        public static RegimeEnum GetRegimeType(string type)
        {
            type = type.Replace(" ", string.Empty).ToLower();
            switch (type)
            {                
                case "sanssel-sanshuile":
                    return RegimeEnum.WITHOUT_SALT_WITHOUT_OIL;
                case "invitéspécial":
                    return RegimeEnum.SPECIAL_GUEST;
                case "membreplein":
                    return RegimeEnum.FULL_MEMBER;
                case "handicapé":
                    return RegimeEnum.DISABLED;
                case "clergé":
                    return RegimeEnum.CLERICAL;
                case "santé":
                    return RegimeEnum.HEALTH_SERVICE;
                case "nouveauné":
                    return RegimeEnum.NEW_BORN;
                case "cuisine":
                    return RegimeEnum.COOKING;
                case "rg":
                    return RegimeEnum.GENERAL_MANAGER;
                case "secondelangue":
                    return RegimeEnum.SECOND_LANGUAGE;
                case "servicechant":
                    return RegimeEnum.SONG_SERVICE;
                case "servicetraduction":
                    return RegimeEnum.TRANSLATION_SERVICE;
                case "serviceinstrument":
                    return RegimeEnum.MUSIC_INSTRUMENT_SERVICE;
                case "religieux":
                    return RegimeEnum.RELIGIOUS;
                case "aucun":                    
                default:
                    return RegimeEnum.NONE;                 
            }
        }

        public static string HallSectionTypeToString(HallSectionTypeEnum type)
        {
            switch (type)
            {
                case HallSectionTypeEnum.CLERICAL:
                    return "Clergé";
                case HallSectionTypeEnum.COOKING:
                    return "Cuisine";
                case HallSectionTypeEnum.DISABLED:
                    return "Handicapé";
                case HallSectionTypeEnum.FULL_MEMBER:
                    return "Membre Plein";
                case HallSectionTypeEnum.GENERAL_MANAGER:
                    return "Responsable Général";
                case HallSectionTypeEnum.HEALTH_SERVICE:
                    return "Service Santé";
                case HallSectionTypeEnum.MUSIC_INSTRUMENT_SERVICE:
                    return "Service Instrument";
                case HallSectionTypeEnum.NEW_BORN:
                    return "Nouveaux-né";
                case HallSectionTypeEnum.RELIGIOUS:
                    return "Religieux";
                case HallSectionTypeEnum.SECOND_LANGUAGE:
                    return "Seconde Langue";
                case HallSectionTypeEnum.SONG_SERVICE:
                    return "Service Chant";
                case HallSectionTypeEnum.SPECIAL_GUEST:
                    return "Invité Spécial";
                case HallSectionTypeEnum.TRANSLATION_SERVICE:
                    return "Service Traduction";
                case HallSectionTypeEnum.NONE:
                default:
                    return "Public";
            }
        }

        public static HallSectionTypeEnum GetHallSectionType(string type)
        {
            type = type.Replace(" ", string.Empty).ToLower();
            switch (type)
            {                
                case "rg":
                    return HallSectionTypeEnum.GENERAL_MANAGER;
                case "servicechant":
                    return HallSectionTypeEnum.SONG_SERVICE;
                case "serviceinstrument":
                    return HallSectionTypeEnum.MUSIC_INSTRUMENT_SERVICE;
                case "servicetraduction":
                    return HallSectionTypeEnum.TRANSLATION_SERVICE;
                case "invitéspécial":
                    return HallSectionTypeEnum.SPECIAL_GUEST;
                case "secondelangue":
                    return HallSectionTypeEnum.SECOND_LANGUAGE;
                case "membreplein":
                    return HallSectionTypeEnum.FULL_MEMBER;
                case "handicapé":
                    return HallSectionTypeEnum.DISABLED;
                case "clergé":
                    return HallSectionTypeEnum.CLERICAL;
                case "santé":
                    return HallSectionTypeEnum.HEALTH_SERVICE;
                case "nouveau-né":
                    return HallSectionTypeEnum.NEW_BORN;
                case "cuisine":
                    return HallSectionTypeEnum.COOKING;
                case "religieux":
                    return HallSectionTypeEnum.RELIGIOUS;
                case "aucun":                    
                default:
                    return HallSectionTypeEnum.NONE;
            }
        }
    }
}
