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

using Newtonsoft.Json;

namespace FirebaseAdmin.Auth
{
  /// <summary>
  /// Represents an error encountered while importing an <c>ImportUserRecordArgs</c>.
  /// </summary>
  public class ErrorInfo
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorInfo"/> class, based on a provided index and reason.
    /// </summary>
    /// <param name="index">The index within the list of errors.</param>
    /// <param name="reason">The reason for the error.</param>
    public ErrorInfo(int index, string reason)
    {
      this.Index = index;
      this.Reason = reason;
    }

    /// <summary>
    /// Gets the index of the failed user in the list passed to the
    /// <see cref="o:FirebaseAuth.ImportUsersAsync"/> method.
    /// </summary>
    /// <returns>An integer index.</returns>
    [JsonProperty("index")]
    public int Index { get; }

    /// <summary>
    /// Gets string describing the error.
    /// </summary>
    /// <returns>A string error message.</returns>
    [JsonProperty("message")]
    public string Reason { get; }
  }
}
