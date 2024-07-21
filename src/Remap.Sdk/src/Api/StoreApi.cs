﻿using Confiti.MoySklad.Remap.Client;
using Confiti.MoySklad.Remap.Entities;
using Confiti.MoySklad.Remap.Queries;
using System.Net.Http;

namespace Confiti.MoySklad.Remap.Api
{
	/// <inheritdoc/>
	public class StoreApi : EntityApiAccessor<Store, ApiParameterBuilder, ApiParameterBuilder>
	{
		#region Ctor

		/// <summary>
		/// Creates a new instance of the <see cref="StoreApi" /> class
		/// with the HTTP client and the MoySklad credentials.
		/// </summary>
		/// <param name="httpClient">The HTTP client.</param>
		/// <param name="credentials">The MoySklad credentials.</param>
		public StoreApi(HttpClient httpClient, MoySkladCredentials credentials)
			: base("/api/remap/1.2/entity/store", httpClient, credentials)
		{
		}

		#endregion Ctor
	}
}