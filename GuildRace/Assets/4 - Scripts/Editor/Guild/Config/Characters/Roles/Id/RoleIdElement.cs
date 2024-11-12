﻿using AD.ToolsCollection;
using System;

namespace Game.Guild
{
    [KeyElement(typeof(RoleId))]
    public class RoleIdElement : KeyElement<string>
    {
        protected override Func<Collection<string>> GetCollection
        {
            get => GuildEditorState.CreateRolesViewCollection;
        }

        protected override void CreateElementGUI(Element root)
        {
            base.CreateElementGUI(root);

            formatSelectedValueOn = false;
        }
    }
}