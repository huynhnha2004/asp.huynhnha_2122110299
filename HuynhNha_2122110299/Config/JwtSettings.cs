﻿namespace HuynhNha_2122110299.Config
{
    public class JwtSettings
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;           
        public int ExpiryMinutes { get; set; } = 60;

    }
}
