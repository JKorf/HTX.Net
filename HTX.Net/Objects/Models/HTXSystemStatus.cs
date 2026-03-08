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
        /// ["<c>page</c>"] Page
        /// </summary>
        [JsonPropertyName("page")]
        public HTXPageInfo Page { get; set; } = null!;
        /// <summary>
        /// ["<c>components</c>"] Components
        /// </summary>
        [JsonPropertyName("components")]
        public HTXSystemComponent[] Components { get; set; } = Array.Empty<HTXSystemComponent>();
        /// <summary>
        /// ["<c>incidents</c>"] Incidents
        /// </summary>
        [JsonPropertyName("incidents")]
        public HTXSystemIncident[] Incidents { get; set; } = Array.Empty<HTXSystemIncident>();
        /// <summary>
        /// ["<c>scheduled_maintenances</c>"] Scheduled maintenances
        /// </summary>
        [JsonPropertyName("scheduled_maintenances")]
        public HTXSystemMaintenance[] ScheduledMaintenances { get; set; } = Array.Empty<HTXSystemMaintenance>();
        /// <summary>
        /// ["<c>status</c>"] Status
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
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>name</c>"] Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>url</c>"] Url
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>time_zone</c>"] Time zone
        /// </summary>
        [JsonPropertyName("time_zone")]
        public string TimeZone { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>updated_at</c>"] Updated at
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
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>name</c>"] Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>status</c>"] System status
        /// </summary>
        [JsonPropertyName("status")]
        public ComponentStatus SystemStatus { get; set; }
        /// <summary>
        /// ["<c>created_at</c>"] Created at
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// ["<c>updated_at</c>"] Updated at
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
        /// <summary>
        /// ["<c>position</c>"] Position
        /// </summary>
        [JsonPropertyName("position")]
        public int Position { get; set; }
        /// <summary>
        /// ["<c>description</c>"] Description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        /// <summary>
        /// ["<c>showcase</c>"] Showcase
        /// </summary>
        [JsonPropertyName("showcase")]
        public bool Showcase { get; set; }
        /// <summary>
        /// ["<c>group_id</c>"] Group id
        /// </summary>
        [JsonPropertyName("group_id")]
        public string GroupId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>page_id</c>"] Page id
        /// </summary>
        [JsonPropertyName("page_id")]
        public string PageId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>group</c>"] Group
        /// </summary>
        [JsonPropertyName("group")]
        public bool Group { get; set; }
        /// <summary>
        /// ["<c>only_show_if_degraded</c>"] Only show if degraded
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
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>name</c>"] Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>status</c>"] Incident status
        /// </summary>
        [JsonPropertyName("status")]
        public IncidentStatus IncidentStatus { get; set; }
        /// <summary>
        /// ["<c>created_at</c>"] Created at
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// ["<c>updated_at</c>"] Updated at
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
        /// <summary>
        /// ["<c>monitoring_at</c>"] Monitoring at
        /// </summary>
        [JsonPropertyName("monitoring_at")]
        public DateTime? MonitoringAt { get; set; }
        /// <summary>
        /// ["<c>resolved_at</c>"] Resolved at
        /// </summary>
        [JsonPropertyName("resolved_at")]
        public DateTime? ResolvedAt { get; set; }
        /// <summary>
        /// ["<c>impact</c>"] Impact
        /// </summary>
        [JsonPropertyName("impact")]
        public string Impact { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>shortlink</c>"] Shortlink
        /// </summary>
        [JsonPropertyName("shortlink")]
        public string Shortlink { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>started_at</c>"] Started at
        /// </summary>
        [JsonPropertyName("started_at")]
        public DateTime StartedAt { get; set; }
        /// <summary>
        /// ["<c>page_id</c>"] Page id
        /// </summary>
        [JsonPropertyName("page_id")]
        public string PageId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>incident_updates</c>"] Incident updates
        /// </summary>
        [JsonPropertyName("incident_updates")]
        public HTXSystemIncidentUpdate[] IncidentUpdates { get; set; } = Array.Empty<HTXSystemIncidentUpdate>();
        /// <summary>
        /// ["<c>components</c>"] Components
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
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>status</c>"] Incident status
        /// </summary>
        [JsonPropertyName("status")]
        public IncidentStatus? IncidentStatus { get; set; }
        /// <summary>
        /// ["<c>body</c>"] Body
        /// </summary>
        [JsonPropertyName("body")]
        public string Body { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>incident_id</c>"] Incident id
        /// </summary>
        [JsonPropertyName("incident_id")]
        public string IncidentId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>created_at</c>"] Created at
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// ["<c>updated_at</c>"] Updated at
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
        /// <summary>
        /// ["<c>display_at</c>"] Display at
        /// </summary>
        [JsonPropertyName("display_at")]
        public DateTime DisplayAt { get; set; }
        /// <summary>
        /// ["<c>affected_components</c>"] Affected components
        /// </summary>
        [JsonPropertyName("affected_components")]
        public HTXSystemIncidentComponent[] AffectedComponents { get; set; } = Array.Empty<HTXSystemIncidentComponent>();
        /// <summary>
        /// ["<c>deliver_notifications</c>"] Deliver notifications
        /// </summary>
        [JsonPropertyName("deliver_notifications")]
        public bool DeliverNotifications { get; set; }
        /// <summary>
        /// ["<c>custom_tweet</c>"] Custom tweet
        /// </summary>
        [JsonPropertyName("custom_tweet")]
        public string? CustomTweet { get; set; }
        /// <summary>
        /// ["<c>tweet_id</c>"] Tweet id
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
        /// ["<c>code</c>"] Code
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>name</c>"] Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>old_status</c>"] Old status
        /// </summary>
        [JsonPropertyName("old_status")]
        public ComponentStatus OldStatus { get; set; }
        /// <summary>
        /// ["<c>new_status</c>"] New status
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
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>name</c>"] Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public MaintenanceStatus Status { get; set; }
        /// <summary>
        /// ["<c>created_at</c>"] Created at
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// ["<c>updated_at</c>"] Updated at
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
        /// <summary>
        /// ["<c>monitoring_at</c>"] Monitoring at
        /// </summary>
        [JsonPropertyName("monitoring_at")]
        public DateTime? MonitoringAt { get; set; }
        /// <summary>
        /// ["<c>resolved_at</c>"] Resolved at
        /// </summary>
        [JsonPropertyName("resolved_at")]
        public DateTime? ResolvedAt { get; set; }
        /// <summary>
        /// ["<c>impact</c>"] Impact
        /// </summary>
        [JsonPropertyName("impact")]
        public string Impact { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>shortlink</c>"] Shortlink
        /// </summary>
        [JsonPropertyName("shortlink")]
        public string Shortlink { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>started_at</c>"] Started at
        /// </summary>
        [JsonPropertyName("started_at")]
        public string StartedAt { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>page_id</c>"] Page id
        /// </summary>
        [JsonPropertyName("page_id")]
        public string PageId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>incident_updates</c>"] Incident updates
        /// </summary>
        [JsonPropertyName("incident_updates")]
        public HTXSystemIncidentUpdate[] IncidentUpdates { get; set; } = Array.Empty<HTXSystemIncidentUpdate>();
        /// <summary>
        /// ["<c>components</c>"] Components
        /// </summary>
        [JsonPropertyName("components")]
        public HTXSystemComponent[] Components { get; set; } = Array.Empty<HTXSystemComponent>();
        /// <summary>
        /// ["<c>scheduled_for</c>"] Scheduled for
        /// </summary>
        [JsonPropertyName("scheduled_for")]
        public DateTime ScheduledFor { get; set; }
        /// <summary>
        /// ["<c>scheduled_until</c>"] Scheduled until
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
        /// ["<c>indicator</c>"] Indicator
        /// </summary>
        [JsonPropertyName("indicator")]
        public SystemStatusIndicator Indicator { get; set; }
        /// <summary>
        /// ["<c>description</c>"] Description
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
    }


}
