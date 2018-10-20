using ResponseMasking.AspNetCore.Filter.Attributes;

namespace ResponseMasking.AspNetCore.Filter.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        [Mask(2, true)]
        public string NameSurname { get; set; }
        public int Age { get; set; }

        [Mask(5)]
        public string CitizenshipNumber { get; set; }
        public UserModel(int id, string userName, string nameSurname, int age, string citizenshipNumber)
        {
            this.Id = id;
            this.UserName = userName;
            this.NameSurname = nameSurname;
            this.Age = age;
            this.CitizenshipNumber = citizenshipNumber;
        }
    }
}