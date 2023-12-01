using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PerpustakaanAppMVC.Model.Entity;
using PerpustakaanAppMVC.Controller;

namespace PerpustakaanAppMVC.View
{
    public partial class FrmEntryMahasiswa : Form
    {

        public delegate void CreateUpdateEventHandler(Mahasiswa mhs);
     
        public event CreateUpdateEventHandler OnCreate;
        
        public event CreateUpdateEventHandler OnUpdate;
       
        private MahasiswaController controller;
       
        private bool isNewData = true;
      
        private Mahasiswa mhs;


        // constructor default
        public FrmEntryMahasiswa()
        {
            InitializeComponent();
        }
        // constructor untuk inisialisasi data ketika entri data baru
        public FrmEntryMahasiswa(string title, MahasiswaController controller)
         : this()
        {
            // ganti text/judul form
            this.Text = title;
            this.controller = controller;
        }
        // constructor untuk inisialisasi data ketika mengedit data
        public FrmEntryMahasiswa(string title, Mahasiswa obj, MahasiswaController
        controller)
         : this()
        {
            // ganti text/judul form
            this.Text = title;
            this.controller = controller;
            isNewData = false; // set status edit data
            mhs = obj; // set objek mhs yang akan diedit
                       // untuk edit data, tampilkan data lama
            txtNpm.Text = mhs.Npm;
            txtNama.Text = mhs.Nama;
            txtAngkatan.Text = mhs.Angkatan;
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            // jika data baru, inisialisasi objek mahasiswa
            if (isNewData) mhs = new Mahasiswa();
            // set nilai property objek mahasiswa yg diambil dari TextBox
            mhs.Npm = txtNpm.Text;
            mhs.Nama = txtNama.Text;
            mhs.Angkatan = txtAngkatan.Text;
            int result = 0;
            if (isNewData) // tambah data baru, panggil method Create
            {
                // panggil operasi CRUD
                result = controller.Create(mhs);
                if (result > 0) // tambah data berhasil
                {
                    OnCreate(mhs); // panggil event OnCreate
                                   // reset form input, utk persiapan input data berikutnya
                    txtNpm.Clear();
                    txtNama.Clear();
                    txtAngkatan.Clear();
                    txtNpm.Focus();
                }
            }
            else // edit data, panggil method Update
            {
                // panggil operasi CRUD
                result = controller.Update(mhs);
                if (result > 0)
                {
                    OnUpdate(mhs); // panggil event OnUpdate
                    this.Close();
                }
            }

        }

        private void btnSelesai_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
