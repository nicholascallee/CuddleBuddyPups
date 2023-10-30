namespace CBP.Models.ViewModels
{
    public class DogApplicationDetailVM
    {
        public DogApplicationDetail DogApplication { get; set; }

        public IEnumerable<Dog>? DogList { get; set; }

        public Dog? CurrentDog { get; set; }
    }
}
