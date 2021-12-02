using System.Collections;

namespace XML_Editor
{
    public partial class Form1 : Form
    {
        Node root;
        HuffmanNode huffmanNode;
        string input, output;
        public Form1()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox2.Clear();
            richTextBox2.AppendText(output);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            huffmanNode = Compression.CreateHuffmanTree(output);
            string y = Compression.HuffmanCompression(output, huffmanNode);
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                BitArray bits = new BitArray(y.Length);
                for (int i = 0; i < y.Length; i++)
                {
                    if (y[i] == '0') bits[i] = false;
                    else bits[i] = true;
                }
                byte[] bytes = new byte[(bits.Length - 1) / 8 + 1];
                bits.CopyTo(bytes, 0);
                using (BinaryWriter binWriter = new BinaryWriter(File.Create(saveFileDialog1.FileName)))
                {
                    binWriter.Write(bytes);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;
                try
                {
                    input = File.ReadAllText(file);
                    richTextBox1.Clear();
                    richTextBox1.AppendText(input);
                    richTextBox2.Clear();
                    output = Consistency.checkConsistency(input);
                    richTextBox2.AppendText(output);
                    root = ParseToTree.ParsingToTree(output);
                    button2.Enabled = true;
                    button3.Enabled = true;
                    button4.Enabled = true;
                    button5.Enabled = true;
                    button6.Enabled = true;
                    /*huffmanNode = Compression.CreateHuffmanTree(x);
                    string y = Compression.HuffmanCompression(x, huffmanNode);
                    BitArray bits = new BitArray(y.Length);
                    for (int i = 0; i < y.Length; i++)
                    {
                        if (y[i] == '0') bits[i] = false;
                        else bits[i] = true;
                    }
                    byte[] bytes = new byte[(bits.Length - 1) / 8 + 1];
                    bits.CopyTo(bytes, 0);
                    richTextBox1.AppendText(Compression.HuffmanCompression(x, huffmanNode));
                    richTextBox2.AppendText(Compression.HuffmanDecompression(y, huffmanNode));*/
                }
                catch (IOException)
                {
                }
            }
        }
    }
}