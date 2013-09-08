﻿using System;
using System.Management.Automation;
using GaPoSh.Services;
using Google.Apis.Admin.Directory.directory_v1.Data;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Set, "GaPoShUserExternalId")]
    public class SetGaPoShUserExternalId : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        [Parameter(Mandatory = true)]
        public string UserId;

        [Parameter(Mandatory = false)] 
        public string Value;
        
        [Parameter(Mandatory = false)]
        public string Type;

        [Parameter(Mandatory = false)] 
        public string CustomType;

        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {
            try
            {
                var user = new User();

                user.ExternalIds[0].CustomType = String.IsNullOrEmpty(CustomType) ? null : CustomType;
                user.ExternalIds[0].Type = String.IsNullOrEmpty(Type) ? null : Type;
                user.ExternalIds[0].Value = String.IsNullOrEmpty(Value) ? null : Value;
                
                var service = request.DirectoryService.Users.Update(user, UserId);

                service.Execute();
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to Update User External Id!");
                Console.WriteLine("Error: " + e);
            }
        }
    }
}