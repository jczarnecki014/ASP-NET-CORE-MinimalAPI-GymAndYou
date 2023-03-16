namespace GymAndYou___MinimalAPI___Project.Gyms;

    public class Gym
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public bool isOpen { get;set; }
    }

