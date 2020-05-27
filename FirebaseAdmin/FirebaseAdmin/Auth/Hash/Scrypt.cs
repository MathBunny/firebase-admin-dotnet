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
using System.Collections.Generic;

namespace FirebaseAdmin.Auth.Hash
{
  /// <summary>
  /// Represents the Scrypt password hashing algorithm. This is the
  /// <a href="https://github.com/firebase/scrypt">modified Scrypt algorithm</a> used by
  /// Firebase Auth. See <a cref="StandardScrypt">StandardScrypt</a> for the standard Scrypt algorithm. 
  /// Can be used as an instance of <a cref="UserImportHash">UserImportHash</a> when importing users.
  /// </summary>
  class Scrypt : RepeatableHash
  {
    protected override string HashName { get { return "SCRYPT"; } }

    protected override int MinRounds { get { return 0; } }
    protected override int MaxRounds { get { return 8; } }

    private string key;
    private string Key
    {
      set
      {
        if (string.IsNullOrEmpty(value))
        {
          throw new ArgumentException("key must not be null or empty");
        }
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(value);
        key = UrlSafeBase64Encode(plainTextBytes);
      }
      get
      {
        if (key == null)
        {
          throw new ArgumentException("key must be initialized");
        }
        return key;
      }
    }

    private string saltSeparator;
    private string SaltSeparator
    {
      set
      {
        if (value != null)
        {
          var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(value);
          saltSeparator = UrlSafeBase64Encode(plainTextBytes);
        }
        else
        {
          saltSeparator = System.Convert.ToBase64String(new byte[0]);
        }
      }
      get
      {
        return saltSeparator;
      }
    }

    private int memoryCost = Int32.MinValue;
    private int MemoryCost
    {
      set
      {
        if (value < 1 || value > 14)
        {
          throw new ArgumentException("memory cost must be between 1 and 14 (inclusive)");
        }
        memoryCost = value;
      }
      get
      {
        if (memoryCost == Int32.MinValue)
        {
          throw new ArgumentException("memory cost must be set");
        }
        return memoryCost;
      }
    }

    protected override IReadOnlyDictionary<string, object> GetOptions()
    {
      var dict = new Dictionary<string, object>((Dictionary<string, object>)(base.GetOptions()));
      dict.Add("signerKey", Key);
      dict.Add("memoryCost", MemoryCost);
      dict.Add("SaltSeparator", SaltSeparator);
      return dict;
    }

    private static string UrlSafeBase64Encode(byte[] bytes)
    {
      var base64Value = Convert.ToBase64String(bytes);
      return base64Value.TrimEnd('=').Replace('+', '-').Replace('/', '_');
    }
  }
}