﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyApi.NetCore.Models
{
    /// <summary>
    /// Full Episode Object.
    /// </summary>
    /// <remarks> https://developer.spotify.com/documentation/web-api/reference/object-model/ </remarks>
    public class PagedEpisodes : Paged<Episode>
    {
    }
}
