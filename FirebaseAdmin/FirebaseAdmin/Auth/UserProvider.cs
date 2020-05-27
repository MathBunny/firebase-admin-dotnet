// Copyright 2020, Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using Newtonsoft.Json;

namespace FirebaseAdmin.Auth
{
  /// <summary>
  /// Represents a user identity provider that can be associated with a Firebase user.
  /// </summary>
  sealed class UserProvider
  {
    [JsonProperty("rawId")]
    private String uid { set; get; }

    [JsonProperty("displayName")]
    private String displayName { set; get; }

    [JsonProperty("email")]
    private String email { set; get; }

    [JsonProperty("photoUrl")]
    private String photoUrl { set; get; }

    [JsonProperty("providerId")]
    private String providerId { set; get; }
  }
}