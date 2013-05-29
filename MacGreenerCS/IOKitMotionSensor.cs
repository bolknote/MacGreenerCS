using System;

namespace MacGreener {
	public struct IOKitMotionSensorCoords {
		public sbyte x, y, z;
	}

	class IOKitMotionSensor : IDisposable {
		IOKitObject connect;

		public IOKitMotionSensor() {
			var matchingDictionary = IOKit.IOServiceMatching ("SMCMotionSensor");

			using (var iterator = IOKit.IOServiceGetMatchingServices(IOKit.IOMasterPort (), matchingDictionary)) {
				using (var service = IOKit.IOIteratorNext (iterator)) {
					connect = IOKit.IOServiceOpen (service);
				}
			}
		}

		public IOKitMotionSensorCoords getCoords() {
			sbyte[] bc = IOKit.IOConnectCallStructMethod (connect);

			return new IOKitMotionSensorCoords {x = bc[0], y = bc[1], z = bc[2]};
		}

		public void Dispose() {
			connect.Dispose ();
		}
	}
}