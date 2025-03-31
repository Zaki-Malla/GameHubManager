namespace GameHubManager.Models.HelperModel
{
    public class ReservationRequest
    {
        public int deviceId { get; set; }
        public DateTime endTime { get; set; }
        public int totalMinutes { get; set; }
        public decimal amountPaid { get; set; }
        public DateTime startTime { get; set; }
    }
}
