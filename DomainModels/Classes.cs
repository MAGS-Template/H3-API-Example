using System.Buffers;

namespace DomainModels
{
    public abstract class Common
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set;}
    }
    public class User : Common
    {
        public string Username { get; set; }
        public string HashedPassword { get; set; }
    }
    public class UserDTO
    {
        public int Id { get; set; } 
        public string Username { get; set; }
    }

    public class RGBData : Common
    {
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
        public int OwnerId { get; set; }

        public User Owner { get; set; }
    }
    public class RGBDataDTO
    {
        public int Id { get; set; }
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
        public int OwnerId { get; set; }
    }

}
