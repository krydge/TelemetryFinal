﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolarisCore.Response {
    public static class Request {
        public static void SetResponse(Dialog d) {

            if (!d.IsSkillsEmpty) {
                if (d.IsRequestingUnimplementedSkill) {
                    d.Response = "Sorry, I recognize that you want me to " + d.Phrase[d.SkillsIndex[0]] + " something, but I don't know how to do it yet.";
                }
                else {
                    if (d.Response == String.Empty)
                        Request.SetGenericSuccessfulResponse(d);
                }
            }
            else {
                d.Response = "Sorry, i'm not quite sure if I understood what you asked me to do.";
            }
            
        }

        public static void SetGenericSuccessfulResponse(Dialog d) {

            d.Response = "Sure thing, i'm going to " + d.Phrase[d.SkillsIndex[0]] + " it for you.";
        }
    }
}
