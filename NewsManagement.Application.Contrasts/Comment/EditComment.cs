﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NewsManagement.Application.Contrasts.Comment
{
	public class EditComment : CreateComment
	{
		[JsonPropertyName("Id")]
		public long Id { get; set; }
		public bool IsConfirm { get; set; }
	}
}
