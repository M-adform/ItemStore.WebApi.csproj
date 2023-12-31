﻿using System.ComponentModel.DataAnnotations;

namespace ItemStore.WebApi.csproj.Models.DTOs.RequestDTOs
{
    public class AddUserRequest
    {
        [Required]
        [MaxLength(100)]
        public string Username { get; set; }
    }
}