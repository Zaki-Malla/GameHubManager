namespace GameHubManager.Models.HelperModel
{
    public class EndReservationRequest
    {
        public int GroupReservationId { get; set; }
        public decimal paidAmountPerDevice { get; set; }
        public decimal duoAmountPerDevice { get; set; }
    }
}
