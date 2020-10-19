﻿//using AssemblyBrowserLib;
//using AssemblyBrowserLib.AssemblyStruct;
//using AssemblyBrowserLib.AssemblyStructView;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace WPFApp
{
    public class ViewModel : INotifyPropertyChanged
    {
        //private AssemblyStructView _assemblyStruct;      
        //public AssemblyStructView AssemblyStruct { 
        //    get 
        //    {
        //        return _assemblyStruct;
        //    }
        //    set 
        //    {
        //        _assemblyStruct = value;
        //        OnPropertyChanged("AssemblyStruct");
        //    } 
        //}

        private RelayCommand _loadAssemblyCommand;
        public RelayCommand LoadAssemblyCommand
        {
            get
            {
                return _loadAssemblyCommand ??
                    (_loadAssemblyCommand = new RelayCommand(obj =>
                    {
                        try
                        {
                            // reading selected assembly
                            OpenFileDialog openFileDialog = new OpenFileDialog();
                            if (openFileDialog.ShowDialog() == true)
                            {
                              //  AssemblyInfo.LoadAssembly(openFileDialog.FileName);
                              //  AssemblyStruct = AssemblyInfo.GetAssemblyInfo();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }));
            }
        }

        //public ViewModel()
        //{
        //    AssemblyInfo.LoadAssembly();
        //    this.AssemblyStruct = AssemblyInfo.GetAssemblyInfo();

        //}

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
