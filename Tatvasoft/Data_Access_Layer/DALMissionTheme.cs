﻿using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Data_Access_Layer
{
    public class DALMissionTheme
    {
        private readonly AppDbContext _context;

        public DALMissionTheme(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<MissionTheme>> GetMissionThemeListAsync()
        {
            return await _context.MissionTheme.Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<MissionTheme> GetMissionThemeByIdAsync(int id)
        {
            return await _context.MissionTheme.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<string> AddMissionThemeAsync(MissionTheme missionTheme)
        {
            try
            {
                _context.MissionTheme.Add(missionTheme);
                await _context.SaveChangesAsync();
                return "Save Theme Successfully.";
            }
            catch (Exception ex)
            {
                throw new Exception("Error in adding theme.", ex);
            }
        }

        public async Task<string> UpdateMissionThemeAsync(MissionTheme missionTheme)
        {
            try
            {
                var existingTheme = await _context.MissionTheme.Where(x => !x.IsDeleted && x.Id == missionTheme.Id).FirstOrDefaultAsync();
                if (existingTheme != null)
                {
                    existingTheme.ThemeName = missionTheme.ThemeName;
                    existingTheme.Status = missionTheme.Status;
                    await _context.SaveChangesAsync();
                    return "Update Theme Successfully.";
                }
                else
                {
                    throw new Exception("Mission Theme is not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in updating theme.", ex);
            }
        }

        public async Task<string> DeleteMissionThemeAsync(int id)
        {
            try
            {
                var existingTheme = await _context.MissionTheme.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
                if (existingTheme != null)
                {
                    existingTheme.IsDeleted = true;
                    await _context.SaveChangesAsync();
                    return "Delete Theme Successfully.";
                }
                else
                {
                    throw new Exception("Mission Theme is not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in deleting theme.", ex);
            }
        }
    }
}
