import { axios } from '@/libs/api.request'
// curl -X 'GET' \
//   'http://localhost:9001/api/DeviceRequest/GetUnHandledCount' \
//   -H 'accept: text/plain' \
//   -H 'request-from: swagger'
export const GetUnHandledCount = () => {
  return axios.get('api/DeviceRequest/GetUnHandledCount')
}

// curl -X 'POST' \
//   'http://localhost:9001/api/DeviceRequest/PostDeviceRequest' \
//   -H 'accept: */*' \
//   -H 'Content-Type: application/json' \
//   -H 'request-from: swagger' \
//   -d '{
// "DeviceName": "string",
//   "Operator": "string",
//   "RequestMessage": "string"
// }'

export const PostDeviceRequest = (data) => {
  return axios.post('api/DeviceRequest/PostDeviceRequest', data)
}

// curl -X 'GET' \
//   'http://localhost:9001/api/DeviceRequest/GetUnHandled' \
//   -H 'accept: text/plain' \
//   -H 'request-from: swagger'

export const GetUnHandled = (data) => {
  return axios.get('api/DeviceRequest/GetUnHandled')
}

// curl -X 'GET' \
//   'http://localhost:9001/api/DeviceRequest/GetDeviceRequests?Start=2021-01-01&End=2024-01-01&DeviceName=%E5%AE%9A%E5%AD%901&Page=1&Size=50' \
//   -H 'accept: text/plain' \
//   -H 'request-from: swagger'
export const GetDeviceRequests = (data) => {
  return axios.get('api/DeviceRequest/GetDeviceRequests', {
    params: data
  })
}

// curl -X 'POST' \
//   'http://localhost:9001/api/DeviceRequest/UpdateDeviceRequest' \
//   -H 'accept: */*' \
//   -H 'Content-Type: application/json' \
//   -H 'request-from: swagger' \
//   -d '{
// "Id": 1,
//   "Operator": "string",
//   "CompletionMessage": "string"
// }'
export const UpdateDeviceRequest = (data) => {
  return axios.post('api/DeviceRequest/UpdateDeviceRequest', data)
}

export default {
  GetUnHandledCount,
  PostDeviceRequest,
  GetUnHandled,
  GetDeviceRequests,
  UpdateDeviceRequest
}
