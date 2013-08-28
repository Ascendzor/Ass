using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using AssClassLibrary;

namespace AssClassLibrary
{
    public class Client
    {
        public int ClientId { get; set; }
        [Required]
        public String Name { get; set; }
        public DateTime DateAdded { get; set; }
        public bool isArchived { get; set; }
        public String Notes { get; set; }
        public DateTime LastModified { get; set; }
        public String LastModifiedBy { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }

    public class Project
    {
        public int ProjectId { get; set; }
        [Required]
        public String Name { get; set; }
        public int ClientId { get; set; }
        public DateTime DateAdded { get; set; }
        public bool isArchived { get; set; }
        public String Notes { get; set; }
        public DateTime LastModified { get; set; }
        public String LastModifiedBy { get; set; }

        public virtual Client Client { get; set; }

        public virtual ICollection<Entry> Entries { get; set; }
    }

    public class Entry
    {
        public int EntryId { get; set; }
        public int ProjectId { get; set; }
        [Required]
        public String Username { get; set; }
        public String Password { get; set; }
        public String Website { get; set; }
        public String Notes { get; set; }
        public DateTime DateAdded { get; set; }
        public bool isArchived { get; set; }
        public DateTime LastModified { get; set; }
        public String LastModifiedBy { get; set; }

        public virtual Project Project { get; set; }
    }
}