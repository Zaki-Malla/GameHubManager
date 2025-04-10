namespace GameHubManager.Models.HelperModel
{
    public class GroupReservationViewModel
    {
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? TotalMinutes { get; set; }
        public decimal? AmountPaid { get; set; }
        public string SelectedDeviceIds { get; set; }
        public bool IsOpenReservation { get; set; }
    }

}
