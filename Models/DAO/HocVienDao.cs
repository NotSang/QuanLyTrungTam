﻿using Models.Framework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class HocVienDao
    {
        private eCenterDbContext context = null;

        public HocVienDao()
        {
            context = new eCenterDbContext();
        }

        public IEnumerable<HocVien> ListAllPaging(string searchString,int page, int pageSize)
        {

            IQueryable<HocVien> model = context.HocViens;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.TenHocVien.Contains(searchString) || x.TenHocVien.Contains(searchString));
            }

            return model.OrderBy(x => x.MaHocVien).ToPagedList(page, pageSize);
        }

        public List<HocVien> ListAll()
        {
            return context.HocViens.ToList();
        }

        public HocVien GetHocVienById(int id)
        {
            return context.HocViens.SingleOrDefault(x => x.MaHocVien == id);
        }

        public int Insert(HocVien entity)
        {
            entity.NgayDangKy = DateTime.Now;
            context.HocViens.Add(entity);
            context.SaveChanges();
            return entity.MaHocVien;
        }

        

        public HocVien ViewDetails(int id)
        {
            return context.HocViens.Find(id);
        }

        public bool Update(HocVien hocVien)
        {
            try
            {
                var _hocVien = context.HocViens.Find(hocVien.MaHocVien);
                _hocVien.TenHocVien = hocVien.TenHocVien;
                _hocVien.GioiTinh = hocVien.GioiTinh;
                _hocVien.SDT = hocVien.SDT;
                _hocVien.Email = hocVien.Email;
                _hocVien.DiaChi = hocVien.DiaChi;
                _hocVien.NgaySinh = hocVien.NgaySinh;
                _hocVien.GhiChu = hocVien.GhiChu;
                _hocVien.TrangThai = hocVien.TrangThai;
                _hocVien.Nguon = hocVien.Nguon;

                context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int maGiaoVien)
        {
            try
            {
                var _hocVien = context.HocViens.Find(maGiaoVien);
                context.HocViens.Remove(_hocVien);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
