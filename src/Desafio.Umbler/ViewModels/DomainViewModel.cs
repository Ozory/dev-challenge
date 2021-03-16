using System;

namespace Desafio.Umbler.ViewModels
{
    public class DomainViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ip { get; set; }
        public string WhoIs { get; set; }
        public string HostedAt { get; set; }
        public bool Valid { get; set; } = true;
    }
}