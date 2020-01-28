using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumApp
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string CensoredText { get; set; }
        public DateTime PostTime { get; set; }
        public bool IsEdited { get; set; }
        public bool ProfanityFilter { get; set; } = false;
        public string EditedString {
            get
            {
                if (IsEdited)
                {
                    return "*Edited*";
                }
                return null;
            }
        }
        public string DisplayText {
            get
            {
                if (ProfanityFilter)
                {
                    return CensoredText;
                }
                else
                {
                    return Text;
                }
            }
        }

    }
}
