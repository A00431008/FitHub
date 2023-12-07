using FitHub.Data;
using FitHub.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace FitHub.Services
{
    public class MembershipManagementService
    {
        private readonly GymDbContext gymDbContext;

        public MembershipManagementService(GymDbContext gymDbContext)
        {
            this.gymDbContext = gymDbContext;
        }

        public bool IsMembershipValid(Membership membership)
        {
            /*validation criteria:
			1. check if the membership type chosen = distinct membershipTypeName from MembershipDetail
			2. 
			*/
            return false;

        }

        public DateTime GetMembershipEndDate(string membTypeId, DateTime StartDate)
        {
            int duration = 0;
            DateTime endDate = DateTime.MinValue;
            try
            {
                var membershipDetail = gymDbContext.MembershipDetail
                    .FirstOrDefault(m => m.MembershipTypeID == membTypeId);

                if (membershipDetail != null)
                {
                    duration = membershipDetail.DurationMonths;
                    endDate = StartDate.AddMonths(duration);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving End Date for the membership type chosen {ex.Message}");
            }

            return endDate;
        }

        /*public int GetMembershipDuration(string memberId)
        {
            int duration = 0;
            try
            {
                var membership = gymDbContext.Membership
                    .FirstOrDefault(m => m.MemberId == memberId);

                if (membership != null)
                {
                    duration = membership.duration;
                    return duration;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving Duration for the membership type {ex.Message}");
            }

            return duration;
        }

        public decimal GetMembershipCost(string memberId)
        {
            decimal cost = 0;
            try
            {
                var membership = gymDbContext.Membership
                    .FirstOrDefault(m => m.MemberId == memberId);

                if (membership != null)
                {
                    cost = membership.cost;
                    return cost;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving Cost for the membership type {ex.Message}");
            }

            return cost;
        }
        
        public void UpdateMembershipStatus(string memberId, bool activate)
         {
             try
             {
                 var membership = gymDbContext.Memberships
                     .FirstOrDefault(m => m.MemberId == memberId);

                 if (membership != null)
                 {
                     membership.IsActive = activate;

                     if (!activate)
                     {
                         membership.ExpiryDate = DateTime.UtcNow;
                     }

                     gymDbContext.SaveChanges();
                 }
             }
             catch (Exception ex)
             {
                 Console.WriteLine($"Error updating membership status: {ex.Message}");
             }
         }

         // Other methods for membership management can be added here...
 */
    }
}
