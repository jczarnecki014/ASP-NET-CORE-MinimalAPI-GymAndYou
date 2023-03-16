namespace GymAndYou___MinimalAPI___Project.Gyms
{
    public class GymService
    {
        private readonly Dictionary<Guid,Gym> _gyms = new();
        public GymService()
        {
            var firstGym = new Gym(){ Name = "DefaultGym" };
            _gyms[firstGym.Id] = firstGym;
        }
        public List<Gym> GetGyms()
        {
            return _gyms.Values.ToList();
        }
        public Gym Get(Guid id)
        {
            return _gyms.GetValueOrDefault(id);
        }
        public void Create(Gym requestGym)
        {
            _gyms[requestGym.Id] = requestGym;
        }
        public void Update(Guid id, Gym requestGym)
        {
            _gyms[id] = requestGym;
        }

        public void Delete(Guid id)
        { 
            var existingGym = this.Get(id);
            if(existingGym is null)
            {
                return;
            }

            _gyms.Remove(existingGym.Id);
        }

    }
}
