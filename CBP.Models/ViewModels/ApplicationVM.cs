namespace CBP.Models.ViewModels
{
    public class ApplicationVM
    {
        public ApplicationDetail ApplicationDetail { get; set; }

        public ApplicationHeader ApplicationHeader { get; set; }

        public IEnumerable<Dog>? DogList { get; set; }

        public Dog? CurrentDog { get; set; }
    }
}
