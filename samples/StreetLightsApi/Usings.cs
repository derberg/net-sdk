﻿// Copyright © 2021-Present Neuroglia SRL. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License"),
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

global using Json.Schema;
global using MQTTnet;
global using MQTTnet.Client;
global using Neuroglia.AsyncApi;
global using Neuroglia.AsyncApi.Bindings.Http;
global using Neuroglia.Data.Schemas.Json;
global using Neuroglia.Serialization;
global using StreetLightsApi.Messages;
global using System.ComponentModel;
global using System.ComponentModel.DataAnnotations;
global using System.Net.Mime;
global using System.Text;
