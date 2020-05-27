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
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace FirebaseAdmin.Auth
{
  /// <summary>
  /// Represents a user account to be imported to Firebase Auth via the
  /// <a cref="o:FirebaseAuth.ImportUsersAsync">FirebaseAuth.ImportUsersAsync</a> API. Must contain at least a
  /// uid string.
  /// </summary>
  public class ImportUserRecordArgs
  {
    string Uid { get; set; }
    string Email { get; set; }
    bool? EmailVerified { get; set; }
    string DisplayName { get; set; }
    string PhoneNumber { get; set; }
    string PhotoUrl { get; set; }
    bool? Disabled
    {
      get; set;
    }
    UserMetadata UserMetadata { get; set; }
    byte[] PasswordHash { get; set; }
    byte[] PasswordSalt { get; set; }
    IEnumerable<UserProvider> UserProviders { get; set; }
    IReadOnlyDictionary<string, object> CustomClaims { get; set; }

    /// <summary>
    /// Determines if a password was set.
    /// </summary>
    /// <returns>bool equivalent to if the PasswordHash is defined.</returns>
    public bool HasPassword()
    {
      return PasswordHash != null;
    }

    /// <summary>
    /// Verifies ImportUserRecordArgs properties by invoking UserRecord validation functions and
    /// returns a dictionary containing the values to be serialied.
    /// </summary>
    /// <returns>Readonly dictionary containing all defined properties.</returns>
    public IReadOnlyDictionary<string, object> GetProperties()
    {
      Dictionary<String, Object> properties = new Dictionary<String, Object>();
      UserRecord.CheckUid(Uid);
      properties.Add("localId", Uid);

      if (!string.IsNullOrEmpty(Email))
      {
        UserRecord.CheckEmail(Email);
        properties.Add("email", Email);
      }

      if (!string.IsNullOrEmpty(PhotoUrl))
      {
        UserRecord.CheckUrl(PhotoUrl);
        properties.Add("photoUrl", PhotoUrl);
      }

      if (!string.IsNullOrEmpty(PhoneNumber))
      {
        UserRecord.CheckPhoneNumber(PhoneNumber);
        properties.Add("phoneNumber", PhoneNumber);
      }

      if (!string.IsNullOrEmpty(DisplayName))
      {
        properties.Add("displayName", DisplayName);
      }

      if (UserMetadata != null)
      {
        if (UserMetadata.CreationTimestamp != null)
        {
          properties.Add("createdAt", UserMetadata.CreationTimestamp);
        }
        if (UserMetadata.LastSignInTimestamp != null)
        {
          properties.Add("lastLoginAt", UserMetadata.LastSignInTimestamp);
        }
      }

      if (PasswordHash != null)
      {
        properties.Add("passwordHash", UrlSafeBase64Encode(PasswordHash));
      }

      if (PasswordSalt != null)
      {
        properties.Add("salt", UrlSafeBase64Encode(PasswordSalt));
      }

      if (UserProviders.Count() > 0)
      {
        properties.Add("providerUserInfo", new List<UserProvider>(UserProviders));
      }

      if (CustomClaims.Count() > 0)
      {
        IReadOnlyDictionary<String, Object> mergedClaims = CustomClaims;
        UserRecord.CheckCustomClaims(mergedClaims);
        properties.Add(UserRecord.CustomAttributes, mergedClaims);
      }

      if (EmailVerified != null)
      {
        properties.Add("emailVerified", EmailVerified);
      }

      if (Disabled != null)
      {
        properties.Add("disabled", Disabled);
      }

      return properties;
    }

    private static string UrlSafeBase64Encode(byte[] bytes)
    {
      var base64Value = Convert.ToBase64String(bytes);
      return base64Value.TrimEnd('=').Replace('+', '-').Replace('/', '_');
    }
  }
}