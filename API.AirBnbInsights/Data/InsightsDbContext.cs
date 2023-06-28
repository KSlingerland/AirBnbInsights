using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API.AirBnbInsights.Models;

public partial class InsightsDbContext : DbContext
{
    public InsightsDbContext()
    {
    }

    public InsightsDbContext(DbContextOptions<InsightsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Listing> Listings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:Default");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Listing>(entity =>
        {
            entity.ToTable("Listing");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Accommodates).HasColumnName("accommodates");
            entity.Property(e => e.Amenities).HasColumnName("amenities");
            entity.Property(e => e.Availability30).HasColumnName("availability_30");
            entity.Property(e => e.Availability365).HasColumnName("availability_365");
            entity.Property(e => e.Availability60).HasColumnName("availability_60");
            entity.Property(e => e.Availability90).HasColumnName("availability_90");
            entity.Property(e => e.Bathrooms)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("bathrooms");
            entity.Property(e => e.BathroomsText)
                .HasMaxLength(50)
                .HasColumnName("bathrooms_text");
            entity.Property(e => e.Bedrooms).HasColumnName("bedrooms");
            entity.Property(e => e.Beds).HasColumnName("beds");
            entity.Property(e => e.CalculatedHostListingsCount).HasColumnName("calculated_host_listings_count");
            entity.Property(e => e.CalculatedHostListingsCountEntireHomes).HasColumnName("calculated_host_listings_count_entire_homes");
            entity.Property(e => e.CalculatedHostListingsCountPrivateRooms).HasColumnName("calculated_host_listings_count_private_rooms");
            entity.Property(e => e.CalculatedHostListingsCountSharedRooms).HasColumnName("calculated_host_listings_count_shared_rooms");
            entity.Property(e => e.CalendarLastScraped)
                .HasColumnType("date")
                .HasColumnName("calendar_last_scraped");
            entity.Property(e => e.CalendarUpdated)
                .HasColumnType("date")
                .HasColumnName("calendar_updated");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.FirstReview)
                .HasColumnType("date")
                .HasColumnName("first_review");
            entity.Property(e => e.HasAvailability)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("has_availability");
            entity.Property(e => e.HostAbout).HasColumnName("host_about");
            entity.Property(e => e.HostAcceptanceRate).HasColumnName("host_acceptance_rate");
            entity.Property(e => e.HostHasProfilePic)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("host_has_profile_pic");
            entity.Property(e => e.HostId).HasColumnName("host_id");
            entity.Property(e => e.HostIdentityVerified)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("host_identity_verified");
            entity.Property(e => e.HostIsSuperhost)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("host_is_superhost");
            entity.Property(e => e.HostListingsCount).HasColumnName("host_listings_count");
            entity.Property(e => e.HostLocation).HasColumnName("host_location");
            entity.Property(e => e.HostName).HasColumnName("host_name");
            entity.Property(e => e.HostNeighbourhood).HasColumnName("host_neighbourhood");
            entity.Property(e => e.HostPictureUrl).HasColumnName("host_picture_url");
            entity.Property(e => e.HostResponseRate).HasColumnName("host_response_rate");
            entity.Property(e => e.HostResponseTime).HasColumnName("host_response_time");
            entity.Property(e => e.HostSince)
                .HasColumnType("date")
                .HasColumnName("host_since");
            entity.Property(e => e.HostThumbnailUrl).HasColumnName("host_thumbnail_url");
            entity.Property(e => e.HostTotalListingsCount).HasColumnName("host_total_listings_count");
            entity.Property(e => e.HostUrl).HasColumnName("host_url");
            entity.Property(e => e.HostVerifications).HasColumnName("host_verifications");
            entity.Property(e => e.InstantBookable)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("instant_bookable");
            entity.Property(e => e.LastReview)
                .HasColumnType("date")
                .HasColumnName("last_review");
            entity.Property(e => e.LastScraped)
                .HasColumnType("datetime")
                .HasColumnName("last_scraped");
            entity.Property(e => e.Latitude)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("latitude");
            entity.Property(e => e.License).HasColumnName("license");
            entity.Property(e => e.ListingUrl).HasColumnName("listing_url");
            entity.Property(e => e.Longitude)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("longitude");
            entity.Property(e => e.MaximumMaximumNights).HasColumnName("maximum_maximum_nights");
            entity.Property(e => e.MaximumMinimumNights).HasColumnName("maximum_minimum_nights");
            entity.Property(e => e.MaximumNights).HasColumnName("maximum_nights");
            entity.Property(e => e.MaximumNightsAvgNtm)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("maximum_nights_avg_ntm");
            entity.Property(e => e.MinimumMaximumNights).HasColumnName("minimum_maximum_nights");
            entity.Property(e => e.MinimumMinimumNights).HasColumnName("minimum_minimum_nights");
            entity.Property(e => e.MinimumNights).HasColumnName("minimum_nights");
            entity.Property(e => e.MinimumNightsAvgNtm)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("minimum_nights_avg_ntm");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.NeighborhoodOverview).HasColumnName("neighborhood_overview");
            entity.Property(e => e.Neighbourhood).HasColumnName("neighbourhood");
            entity.Property(e => e.NeighbourhoodCleansed).HasColumnName("neighbourhood_cleansed");
            entity.Property(e => e.NeighbourhoodGroupCleansed).HasColumnName("neighbourhood_group_cleansed");
            entity.Property(e => e.NumberOfReviews).HasColumnName("number_of_reviews");
            entity.Property(e => e.NumberOfReviewsL30d).HasColumnName("number_of_reviews_l30d");
            entity.Property(e => e.NumberOfReviewsLtm).HasColumnName("number_of_reviews_ltm");
            entity.Property(e => e.PictureUrl).HasColumnName("picture_url");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.PropertyType).HasColumnName("property_type");
            entity.Property(e => e.ReviewScoresAccuracy).HasColumnName("review_scores_accuracy");
            entity.Property(e => e.ReviewScoresCheckin).HasColumnName("review_scores_checkin");
            entity.Property(e => e.ReviewScoresCleanliness).HasColumnName("review_scores_cleanliness");
            entity.Property(e => e.ReviewScoresCommunication).HasColumnName("review_scores_communication");
            entity.Property(e => e.ReviewScoresLocation).HasColumnName("review_scores_location");
            entity.Property(e => e.ReviewScoresRating).HasColumnName("review_scores_rating");
            entity.Property(e => e.ReviewScoresValue).HasColumnName("review_scores_value");
            entity.Property(e => e.ReviewsPerMonth)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("reviews_per_month");
            entity.Property(e => e.RoomType).HasColumnName("room_type");
            entity.Property(e => e.ScrapeId).HasColumnName("scrape_id");
            entity.Property(e => e.Source).HasColumnName("source");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
