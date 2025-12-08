using HTX.Net.Enums;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// System status info
    /// </summary>
    [SerializationModel]
    public record HTXSystemStatus
    {
        /// <summary>
        /// Page
        /// </summary>
        [JsonPropertyName("page")]
        public HTXPageInfo Page { get; set; } = null!;
        /// <summary>
        /// Components
        /// </summary>
        [JsonPropertyName("components")]
        public HTXSystemComponent[] Components { get; set; } = Array.Empty<HTXSystemComponent>();
        /// <summary>
        /// Incidents
        /// </summary>
        [JsonPropertyName("incidents")]
        public HTXSystemIncident[] Incidents { get; set; } = Array.Empty<HTXSystemIncident>();
        /// <summary>
        /// Scheduled maintenances
        /// </summary>
        [JsonPropertyName("scheduled_maintenances")]
        public HTXSystemMaintenance[] ScheduledMaintenances { get; set; } = Array.Empty<HTXSystemMaintenance>();
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public HTXSystemStatusIndicator? Status { get; set; }
    }

    /// <summary>
    /// Page info
    /// </summary>
    [SerializationModel]
    public record HTXPageInfo
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Url
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
        /// <summary>
        /// Time zone
        /// </summary>
        [JsonPropertyName("time_zone")]
        public string TimeZone { get; set; } = string.Empty;
        /// <summary>
        /// Updated at
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }

    /// <summary>
    /// System component
    /// </summary>
    [SerializationModel]
    public record HTXSystemComponent
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// System status
        /// </summary>
        [JsonPropertyName("status")]
        public ComponentStatus SystemStatus { get; set; }
        /// <summary>
        /// Created at
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Updated at
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
        /// <summary>
        /// Position
        /// </summary>
        [JsonPropertyName("position")]
        public int Position { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        /// <summary>
        /// Showcase
        /// </summary>
        [JsonPropertyName("showcase")]
        public bool Showcase { get; set; }
        /// <summary>
        /// Group id
        /// </summary>
        [JsonPropertyName("group_id")]
        public string GroupId { get; set; } = string.Empty;
        /// <summary>
        /// Page id
        /// </summary>
        [JsonPropertyName("page_id")]
        public string PageId { get; set; } = string.Empty;
        /// <summary>
        /// Group
        /// </summary>
        [JsonPropertyName("group")]
        public bool Group { get; set; }
        /// <summary>
        /// Only show if degraded
        /// </summary>
        [JsonPropertyName("only_show_if_degraded")]
        public bool OnlyShowIfDegraded { get; set; }
    }

    /// <summary>
    /// System incident
    /// </summary>
    [SerializationModel]
    public record HTXSystemIncident
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Incident status
        /// </summary>
        [JsonPropertyName("status")]
        public IncidentStatus IncidentStatus { get; set; }
        /// <summary>
        /// Created at
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Updated at
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
        /// <summary>
        /// Monitoring at
        /// </summary>
        [JsonPropertyName("monitoring_at")]
        public DateTime? MonitoringAt { get; set; }
        /// <summary>
        /// Resolved at
        /// </summary>
        [JsonPropertyName("resolved_at")]
        public DateTime? ResolvedAt { get; set; }
        /// <summary>
        /// Impact
        /// </summary>
        [JsonPropertyName("impact")]
        public string Impact { get; set; } = string.Empty;
        /// <summary>
        /// Shortlink
        /// </summary>
        [JsonPropertyName("shortlink")]
        public string Shortlink { get; set; } = string.Empty;
        /// <summary>
        /// Started at
        /// </summary>
        [JsonPropertyName("started_at")]
        public DateTime StartedAt { get; set; }
        /// <summary>
        /// Page id
        /// </summary>
        [JsonPropertyName("page_id")]
        public string PageId { get; set; } = string.Empty;
        /// <summary>
        /// Incident updates
        /// </summary>
        [JsonPropertyName("incident_updates")]
        public HTXSystemIncidentUpdate[] IncidentUpdates { get; set; } = Array.Empty<HTXSystemIncidentUpdate>();
        /// <summary>
        /// Components
        /// </summary>
        [JsonPropertyName("components")]
        public HTXSystemComponent[] Components { get; set; } = Array.Empty<HTXSystemComponent>();
    }

    /// <summary>
    /// System incident update
    /// </summary>
    [SerializationModel]
    public record HTXSystemIncidentUpdate
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Incident status
        /// </summary>
        [JsonPropertyName("status")]
        public IncidentStatus? IncidentStatus { get; set; }
        /// <summary>
        /// Body
        /// </summary>
        [JsonPropertyName("body")]
        public string Body { get; set; } = string.Empty;
        /// <summary>
        /// Incident id
        /// </summary>
        [JsonPropertyName("incident_id")]
        public string IncidentId { get; set; } = string.Empty;
        /// <summary>
        /// Created at
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Updated at
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
        /// <summary>
        /// Display at
        /// </summary>
        [JsonPropertyName("display_at")]
        public DateTime DisplayAt { get; set; }
        /// <summary>
        /// Affected components
        /// </summary>
        [JsonPropertyName("affected_components")]
        public HTXSystemIncidentComponent[] AffectedComponents { get; set; } = Array.Empty<HTXSystemIncidentComponent>();
        /// <summary>
        /// Deliver notifications
        /// </summary>
        [JsonPropertyName("deliver_notifications")]
        public bool DeliverNotifications { get; set; }
        /// <summary>
        /// Custom tweet
        /// </summary>
        [JsonPropertyName("custom_tweet")]
        public string? CustomTweet { get; set; }
        /// <summary>
        /// Tweet id
        /// </summary>
        [JsonPropertyName("tweet_id")]
        public string? TweetId { get; set; }
    }

    /// <summary>
    /// System incident affacted component
    /// </summary>
    [SerializationModel]
    public record HTXSystemIncidentComponent
    {
        /// <summary>
        /// Code
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Old status
        /// </summary>
        [JsonPropertyName("old_status")]
        public ComponentStatus OldStatus { get; set; }
        /// <summary>
        /// New status
        /// </summary>
        [JsonPropertyName("new_status")]
        public ComponentStatus NewStatus { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [SerializationModel]
    public record HTXSystemMaintenance
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public MaintenanceStatus Status { get; set; }
        /// <summary>
        /// Created at
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Updated at
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
        /// <summary>
        /// Monitoring at
        /// </summary>
        [JsonPropertyName("monitoring_at")]
        public DateTime? MonitoringAt { get; set; }
        /// <summary>
        /// Resolved at
        /// </summary>
        [JsonPropertyName("resolved_at")]
        public DateTime? ResolvedAt { get; set; }
        /// <summary>
        /// Impact
        /// </summary>
        [JsonPropertyName("impact")]
        public string Impact { get; set; } = string.Empty;
        /// <summary>
        /// Shortlink
        /// </summary>
        [JsonPropertyName("shortlink")]
        public string Shortlink { get; set; } = string.Empty;
        /// <summary>
        /// Started at
        /// </summary>
        [JsonPropertyName("started_at")]
        public string StartedAt { get; set; } = string.Empty;
        /// <summary>
        /// Page id
        /// </summary>
        [JsonPropertyName("page_id")]
        public string PageId { get; set; } = string.Empty;
        /// <summary>
        /// Incident updates
        /// </summary>
        [JsonPropertyName("incident_updates")]
        public HTXSystemIncidentUpdate[] IncidentUpdates { get; set; } = Array.Empty<HTXSystemIncidentUpdate>();
        /// <summary>
        /// Components
        /// </summary>
        [JsonPropertyName("components")]
        public HTXSystemComponent[] Components { get; set; } = Array.Empty<HTXSystemComponent>();
        /// <summary>
        /// Scheduled for
        /// </summary>
        [JsonPropertyName("scheduled_for")]
        public DateTime ScheduledFor { get; set; }
        /// <summary>
        /// Scheduled until
        /// </summary>
        [JsonPropertyName("scheduled_until")]
        public DateTime ScheduledUntil { get; set; }
    }

    /// <summary>
    /// Indicator
    /// </summary>
    [SerializationModel]
    public record HTXSystemStatusIndicator
    {
        /// <summary>
        /// Indicator
        /// </summary>
        [JsonPropertyName("indicator")]
        public SystemStatusIndicator Indicator { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
    }


}
