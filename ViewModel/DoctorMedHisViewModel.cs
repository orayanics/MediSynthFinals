﻿using MediSynthFinals.Models;

namespace MediSynthFinals.ViewModel
{
    public class DoctorMedHisViewModel
    {
        public int medhistoryId { get; set; }
        public string pastMedHistory { get; set; }
        public string pastHospitalization { get; set; }
        public string pastSurgicalOperation { get; set; }
        public string medConcern { get; set; }
        public string foodAllergy { get; set; }
        public string drugAllergy { get; set; }
        public string attendingDoctor { get; set; }
        public DateTime visitDate { get; set; }

        // FOREIGN KEYS
        public string rtypeId { get; set; }
        public string patientId { get; set; }
    }
}
