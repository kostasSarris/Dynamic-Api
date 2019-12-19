using System;
using System.Collections.Generic;

namespace TPDMS.RestApi.Models
{
    public class C33Dispatch
    {
        public C33Dispatch()
        {
            C33Dispatch_aUIs = new HashSet<CaUIs>();
            C33Dispatch_upUIs = new HashSet<CupUIs>();
        }

        public int Record_Type { get; set; }
        public DateTime File_Date { get; set; }
        public string File_ID { get; set; }
        public string Dispatch_ID { get; set; }
        public string Order_Numbe { get; set; }
        public string Message_Type { get; } = "3-3";
        public string EO_ID { get; set; }
        public string Event_Time { get; set; }
        public string F_ID { get; set; }
        public int Destination_ID1 { get; set; }
        public string Destination_ID2 { get; set; }
        public string Destination_ID5 { get; set; }
        public int Transport_mode { get; set; }
        public string Transport_vehicle { get; set; }
        public bool Transport_cont1 { get; set; }
        public string Transport_cont2 { get; set; }
        public bool Transport_s1 { get; set; }
        public string Transport_s2 { get; set; }
        public bool EMCS { get; set; }
        public string EMCS_ARC { get; set; }
        public bool SAAD { get; set; }
        public string SAAD_number { get; set; }
        public bool Exp_Declaration { get; set; }
        public string Exp_DeclarationNumber { get; set; }
        public int UI_Type { get; set; }
        public string Dispatch_comment { get; set; }
        public virtual ICollection<CaUIs> C33Dispatch_aUIs { get; set; }

        public virtual ICollection<CupUIs> C33Dispatch_upUIs { get; set; }
    }

    public partial class CupUIs
    {
        public long upUIsId { get; set; }

        public long Id { get; set; }

        public string upUIs { get; set; }
    }

    public partial class CaUIs
    {
        public long aUIsId { get; set; }

        public long Id { get; set; }

        public string aUIs { get; set; }
    }
}