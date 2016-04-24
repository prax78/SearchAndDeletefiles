/*
 * Created by SharpDevelop.
 * User: narayanan
 * Date: 4/23/2016
 * Time: 2:01 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace listfiles
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{public string Filter;
		public double days=0;
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void Button1Click(object sender, EventArgs e)
		{
			
			if(listBox1.Items.Count>0)
			{
				listBox1.Items.Clear();
				
			}
			
			if(radioButton1.Checked)
			   {
				
					if(textBox1.Text!="")
						{
								button2.Enabled=true;
								button4.Enabled=true;
								string selection=folderBrowserDialog1.ShowDialog().ToString();
								if(selection!=DialogResult.Cancel.ToString())
								{
								string path=folderBrowserDialog1.SelectedPath;
								Filter=textBox1.Text.ToString();	
							
			 												
			 					 getsubfiles(path);	 	
			 				
							    label1.Text="Total Object Found:"+listBox1.Items.Count;
								}
						}
					else
						{
							MessageBox.Show("file filter option should not be blank",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Error);
						}
				}
			
			
			if(radioButton2.Checked)
			{
				
				if(textBox2.Text!="")
					{
						
						
								button2.Enabled=true;
								button4.Enabled=true;
								string selection1=folderBrowserDialog1.ShowDialog().ToString();
								if(selection1!=DialogResult.Cancel.ToString())
								{
								string path1=folderBrowserDialog1.SelectedPath;
								days=Convert.ToDouble( textBox2.Text);
								if((days <0) || (days > 6000))
								{
									MessageBox.Show("Enter Value between 0 and 6000",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Error);
								}
								   
								days=(-1*days);
			 			getsubfiles1(path1);
			 			 label1.Text="Total Files Found:"+listBox1.Items.Count;
								}
					}
				else
						{
							MessageBox.Show("file filter option should not be blank",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Error);
						}
				}
			
		}
		
		void getsubfiles(string p)
		{
			try
			{
			foreach(string d in System.IO.Directory.GetDirectories(p))
			{
				getsubfiles(d);
			}
				
			foreach(string f in System.IO.Directory.GetFiles(p,Filter))
			{
				listBox1.Items.Add(f);
			}
			}
			catch(Exception e)
			{
				listBox1.Items.Add(e.Message);
			}
			
		}
		void Button2Click(object sender, EventArgs e) 
		{
			//saveFileDialog1.ShowDialog();
			
			if(listBox1.Items.Count!=0)
			{
			string diagresult=saveFileDialog1.ShowDialog().ToString();
			string filename=saveFileDialog1.FileName;
			if(diagresult!=DialogResult.Cancel.ToString())
			{
			System.IO.StreamWriter wrf=new System.IO.StreamWriter(filename);
			
			foreach(object item in listBox1.Items)
			{
				wrf.WriteLine(item);
			}
		
			wrf.Close();
			
			wrf.Dispose();
			MessageBox.Show("File Saved "+filename,this.Text,MessageBoxButtons.OK,MessageBoxIcon.Information);
			}
			else
			{
				MessageBox.Show("you have not selected files",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
			}
			else{
				MessageBox.Show("List is empty",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}
		void Button3Click(object sender, EventArgs e)
		{
			listBox1.Items.Clear();
			label1.Text="";
			button2.Enabled=false;
			button4.Enabled=false;
			textBox1.Text="";
			textBox2.Text="";
			radioButton1.Enabled=true;
			radioButton2.Enabled=true;
			textBox1.Enabled=true;
			textBox2.Enabled=true;
		
		}
		void MainFormLoad(object sender, EventArgs e)
		{
	if(listBox1.Items.Count==0)
			{
				button2.Enabled=false;
				button4.Enabled=false;
			}
	

		}
		void Button4Click(object sender, EventArgs e)
		{
			System.Text.StringBuilder sb=new System.Text.StringBuilder();
			if(listBox1.Items.Count!=0)
			{
			string msgres=MessageBox.Show("You are about to delete files listed here,Select Yes to delete files or No to close application",this.Text,MessageBoxButtons.YesNo,MessageBoxIcon.Error,MessageBoxDefaultButton.Button2).ToString();
			if(msgres=="Yes")
			{
				foreach(object item in listBox1.Items)
				{try{
					System.IO.File.Delete(item.ToString());
					}
					catch(Exception ex){
						sb.Append(ex.Message.ToString());
						sb.Append(Environment.NewLine);
					}
				}
				if(sb.Length!=0)
				{
				MessageBox.Show(sb.ToString(),this.Text,MessageBoxButtons.OK,MessageBoxIcon.Error);
				}
				MessageBox.Show("Listed files are Deleted",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Information);
			}
			else
			{
				this.Close();
				
			}
			}
			else
			{
				MessageBox.Show("List is empty",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
			
		}
		
		void getsubfiles1(string m)
		{
			try
			{
			foreach(string d in System.IO.Directory.GetDirectories(m))
			{
				getsubfiles1(d);
			}
			
			foreach(string f in System.IO.Directory.GetFiles(m))
			{
				if(System.IO.File.GetLastWriteTime(f)< System.DateTime.Now.AddDays(days))
				{
				listBox1.Items.Add(f);
				}
			}
			
			}
			catch(Exception e)
			{
				listBox1.Items.Add(e.Message);
			}
			
		}
		void RadioButton2CheckedChanged(object sender, EventArgs e)
		{
			
				textBox1.Enabled=false;
				textBox2.Enabled=true;
				
		}
		void RadioButton1CheckedChanged(object sender, EventArgs e)
		{
	
				textBox2.Enabled=false;
				textBox1.Enabled=true;
				
		}
		void Timer1Tick(object sender, EventArgs e)
		{
			
			
			
			
			
		}
		void Timer2Tick(object sender, EventArgs e)
		{
	
		}
		
		
	}
}

