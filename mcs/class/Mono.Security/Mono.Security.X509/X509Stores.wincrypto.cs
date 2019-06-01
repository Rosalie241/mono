//
// X509Stores.cs: Handles X.509 certificates/CRLs stores group.
//
// Author:
//	Sebastien Pouliot  <sebastien@ximian.com>
//
// (C) 2004 Novell (http://www.novell.com)
//

//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.Collections;
using System.IO;

using Mono.Security.X509.Extensions;

using StoreLocation = System.Security.Cryptography.X509Certificates.StoreLocation;

namespace Mono.Security.X509 {

#if INSIDE_CORLIB || INSIDE_SYSTEM
	internal
#else
	public 
#endif
	class X509Stores {

		private StoreLocation _location;
		private X509Store _personal;
		private X509Store _other;
		private X509Store _intermediate;
		private X509Store _trusted;
		private X509Store _untrusted;

		internal X509Stores (StoreLocation location)
		{
			_location = location;
		}

		// properties

		public X509Store Personal {
			get { 
				if (_personal == null) {
					_personal = new X509Store (Names.Personal, _location);
				}
				return _personal; 
			}
		}

		public X509Store OtherPeople {
			get { 
				if (_other == null) {
					_other = new X509Store (Names.OtherPeople, _location);
				}
				return _other; 
			}
		}

		public X509Store IntermediateCA {
			get { 
				if (_intermediate == null) {
					_intermediate = new X509Store (Names.IntermediateCA, _location);
				}
				return _intermediate; 
			}
		}

		public X509Store TrustedRoot {
			get { 
				if (_trusted == null) {
					_trusted = new X509Store (Names.TrustedRoot, _location);
				}
				return _trusted; 
			}
		}

		public X509Store Untrusted {
			get { 
				if (_untrusted == null) {
					_untrusted = new X509Store (Names.Untrusted, _location);
				}
				return _untrusted; 
			}
		}

		// methods

		public void Clear () 
		{
			// this will force a reload of all stores
			if (_personal != null)
				_personal.Clear ();
			_personal = null;
			if (_other != null)
				_other.Clear ();
			_other = null;
			if (_intermediate != null)
				_intermediate.Clear ();
			_intermediate = null;
			if (_trusted != null)
				_trusted.Clear ();
			_trusted = null;
			if (_untrusted != null)
				_untrusted.Clear ();
			_untrusted = null;
		}

		public X509Store Open (string storeName, bool create)
		{
			if (storeName == null)
				throw new ArgumentNullException ("storeName");

			return new X509Store (storeName, _location);
		}

		// names

		public class Names {

			// do not translate
			public const string Personal = "My";
			public const string OtherPeople = "AddressBook";
			public const string IntermediateCA = "CA";
			public const string TrustedRoot = "Trust";
			public const string Untrusted = "Disallowed";

			public Names () {}
		}
	}
}
