namespace ResourceBookingCOJ.Models
{
    //view model to hold both models for resource creation as well as resource list for display
    public class ResourceViewModel
    {
        public Resource Resource { get; set; } = new Resource();
        public List<Resource> ResourceList { get; set; } = new List<Resource>();
    }
}
