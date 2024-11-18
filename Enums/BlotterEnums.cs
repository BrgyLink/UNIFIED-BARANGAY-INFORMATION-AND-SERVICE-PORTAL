using System.ComponentModel.DataAnnotations;

namespace BrgyLink.Enums
{
    public enum BlotterType
    {
        [Display(Name = "Assault")]
        Assault,

        [Display(Name = "Theft")]
        Theft,

        [Display(Name = "Dispute")]
        Dispute,

        [Display(Name = "Harassment")]
        Harassment,

        [Display(Name = "Vandalism")]
        Vandalism,

        [Display(Name = "Disturbance")]
        Disturbance,

        [Display(Name = "Other")]
        Other
    }

    public enum BlotterStatus
    {
        [Display(Name = "Pending")]
        Pending,

        [Display(Name = "Under Investigation")]
        UnderInvestigation,

        [Display(Name = "In Mediation")]
        Mediation,

        [Display(Name = "Resolved")]
        Resolved,

        [Display(Name = "Dismissed")]
        Dismissed,

        [Display(Name = "Transferred")]
        Transferred,

        [Display(Name = "Closed")]
        Closed
    }

}