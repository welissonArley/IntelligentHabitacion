﻿namespace IntelligentHabitacion.Communication.Response
{
    public class ResponseLoginJson
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsPartOfOneHome { get; set; }
        public bool IsAdministrator { get; set; }
        public string ProfileColor { get; set; }
    }
}