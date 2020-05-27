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
  /// Represents the Standard Scrypt password hashing algorithm. Can be used as an instance of
  /// <a cref="UserImportHash">UserImportHash</a> when importing users.
  /// </summary>
  class StandardScrypt : UserImportHash
  {
    protected override string HashName { get { return "STANDARD_SCRYPT"; } }

    private int? derivedKeyLength;
    private int DerivedKeyLength
    {
      set
      {
        if (value < 0)
        {
          throw new ArgumentException("DerivedKeyLength must be non-negative");
        }
        derivedKeyLength = value;
      }
      get
      {
        if (derivedKeyLength == null)
        {
          throw new ArgumentException("DerivedKeyLength must be initialized");
        }
        return (int)derivedKeyLength;
      }
    }

    private int? blockSize;
    private int BlockSize
    {
      set
      {
        if (value < 0)
        {
          throw new ArgumentException("BlockSize must be non-negative");
        }
        blockSize = value;
      }
      get
      {
        if (blockSize == null)
        {
          throw new ArgumentException("BlockSize must be initialized");
        }
        return (int)blockSize;
      }
    }

    private int? parallelization;
    private int Parallelization
    {
      set
      {
        if (value < 0)
        {
          throw new ArgumentException("Parallelization must be non-negative");
        }
        parallelization = value;
      }
      get
      {
        if (parallelization == null)
        {
          throw new ArgumentException("Parallelization must be initialized");
        }
        return (int)parallelization;
      }
    }

    private int? memoryCost;
    private int MemoryCost
    {
      set
      {
        if (value < 0)
        {
          throw new ArgumentException("Memory cost must be non-negative");
        }
        memoryCost = value;
      }
      get
      {
        if (memoryCost == null)
        {
          throw new ArgumentException("Memory cost must be initialized");
        }
        return (int)memoryCost;
      }
    }

    protected override IReadOnlyDictionary<string, object> GetOptions()
    {
      var dict = new Dictionary<string, object>();
      dict.Add("dkLen", DerivedKeyLength);
      dict.Add("blockSize", BlockSize);
      dict.Add("parallization", Parallelization);
      dict.Add("memoryCost", MemoryCost);
      return dict;
    }
  }
}