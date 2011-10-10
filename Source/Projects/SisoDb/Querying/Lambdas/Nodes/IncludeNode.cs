﻿using System;
using EnsureThat;

namespace SisoDb.Querying.Lambdas.Nodes
{
    [Serializable]
    public class IncludeNode : INode
    {
        public string ChildStructureName { get; private set; }

        public string IdReferencePath { get; private set; }

        public string ObjectReferencePath { get; private set; }

        public IncludeNode(string childStructureName, string idReferencePath, string objectReferencePath)
        {
            Ensure.That(() => childStructureName).IsNotNullOrWhiteSpace();
            Ensure.That(() => idReferencePath).IsNotNullOrWhiteSpace();
            Ensure.That(() => objectReferencePath).IsNotNullOrWhiteSpace();

            ChildStructureName = childStructureName;
            IdReferencePath = idReferencePath;
            ObjectReferencePath = objectReferencePath;
        }
    }
}