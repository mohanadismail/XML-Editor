﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XML_Editor
{
    internal class Node
    {
        /* 
         * <tag> // Parent depth = 1
         *      <tag> data </tag> // Child, depth = 0
         * <tag> 
         */
        private string tag;
        private string? data;
        private Node? parent;
        private List<Node> children;
        private int depth;

        /* Construct a NODE */
        public Node(string tag, string? data)
        {
            this.tag = tag;
            this.data = data;
            this.parent = null;
            this.depth = 0;
            this.children = new List<Node>();
        }


        /* Appends a node into the children list */
        public void addChild(Node child_node)
        {
            // giving the child a parent
            child_node.parent = this;
            // the depth of the child_node is more than the parent's by 1
            child_node.depth = this.depth + 1;
            // giving the parent a child
            children.Append(child_node);
        }


        /* GETTERS */
        public Node? getParent()
        {
            return parent;
        }
        public List<Node> getChildren()
        {
            return children;
        }
        public string getTag()
        {
            return tag;
        }
        public string? getData()
        {
            return data;
        }
        public int getDepth()
        {
            return depth;
        }
    }
}
