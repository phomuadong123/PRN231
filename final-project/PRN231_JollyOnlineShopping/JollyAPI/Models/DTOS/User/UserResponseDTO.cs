﻿namespace JollyAPI.Models.DTOS.User
{
    public class UserResponseDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool? Status { get; set; }
    }
}
