﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XML_Editor
{
    internal class Consistency
    {
        public static string checkConsistency(string s, ref int errors)
        {
            Stack<string> st = new Stack<string>();
            string output = "";
            int index = 0;
            errors = 0;
            bool flagfollower = false;
            bool flagendtagfollower = false;
            while (index < s.Length)
            {
                if (s[index] == '<')
                {
                    int open = findChar(s, index, '<');
                    if (s[open + 1] != '/')
                    { //openning tag 

                        int close = findChar(s, open + 1, '>');
                        string str = s.Substring(open + 1, close - open - 1);//without <>
                        if (str == "follower")
                        {
                            st.Push(str);
                            output += s.Substring(index, close - index + 1);
                            index = close;
                            index++;
                            flagfollower = true;
                            continue;

                        }
                        if (flagfollower)
                        {
                            if (str == "id")
                            {
                                st.Push(str);
                                output += s.Substring(index, close - index + 1);
                                index = close;
                                index++;
                                flagfollower = false;
                                continue;
                            }
                            else
                            {
                                output += "<id>";
                                index = close;
                                index++;
                                flagfollower = false;
                                flagendtagfollower = true;
                                errors++;
                                continue;
                            }
                        }
                        else
                        {
                            st.Push(str);
                            output += s.Substring(index, close - index + 1);
                            index = close;
                            index++;
                            continue;
                        }
                    }

                    else
                    { //closing tag
                        int slash = findChar(s, index, '/');
                        int close = findChar(s, index, '>');
                        string str = s.Substring(slash + 1, close - slash - 1);
                        if (st.Contains(str))
                        {
                            if (str == st.Peek())
                            { //no error
                                output += "<" + "/" + st.Peek() + ">";
                                st.Pop();
                            }
                        }
                        else
                        { //error
                            if (st.Contains(str))
                            {
                                while (str != st.Peek())
                                {
                                    output += "<" + "/" + st.Peek() + ">" + "\n";
                                    st.Pop();
                                    errors++;
                                }
                                output += "<" + "/" + st.Peek() + ">" + "\n";
                                st.Pop();
                            }

                        }
                        index = close;
                        index++;
                        continue;
                    }
                }

                if (s[index] != '<' && s[index] != '\n' && s[index] != ' ' && s[index] != '\r')
                { //data
                    int open = findChar(s, index, '<');
                    int length = open - index;
                    output += s.Substring(index, length);
                    index = open - 1;
                    index++;
                    string check = st.Peek();
                    int start = findChar(s, index, '<');
                    if (check == s.Substring(start + 2, st.Peek().Length))
                    {
                        output += "</" + st.Peek() + ">";
                        index = index + st.Peek().Length + 3;
                        st.Pop();
                    }
                    else if (flagendtagfollower)
                    {

                        int openflag = findChar(s, index, '<');
                        int closeflag = findChar(s, index, '>');
                        int lengthflag = closeflag - openflag + 1;
                        index = index + lengthflag;
                        output += "</id>";
                        flagendtagfollower = false;
                        continue;
                    }
                    else
                    {
                        output += "</" + st.Peek() + ">" + "\n";
                        st.Pop();
                        errors++;
                    }
                    continue;
                }
                if (s[index] == '\n')
                {
                    output += "\n";
                    index++;
                    while (s[index] == ' ')
                    {
                        output += s[index];
                        index++;
                    }
                }
                if (s[index] == '\r')
                {
                    output += "\r";
                    index++;
                }
                if (s[index] == ' ')
                {
                    output += s[index];
                }

            }
            if (st.Count != 0)
            {
                while (st.Count != 0)
                {
                    output += "</" + st.Peek() + ">";
                    st.Pop();
                }
            }
            return output;

        }
        static int findChar(string s,int index,char a) {
            for (int i = index; i < s.Length; i++) {
                if (a == s[i])
                    return i;
            }
            return -1;  
        }
        static int findBetween(string s,int start, int end, char a) {
            for (int i = start; i < end; i++) {
                if (a == s[i])
                    return i;
            }
            return 0;
        }

    }

}
