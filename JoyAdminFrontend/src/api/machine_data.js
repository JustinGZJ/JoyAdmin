import { axios } from '@/libs/api.request'

export const GetMachineDataByCode = (code, params) => {
  return axios.get(`api/MachineData/Get/${code}`)
}
// http://localhost:9001/api/MachineData/BindShellCode
/// <summary>
/// 绑定机器码
/// </summary>
/// <param name="data">{
///  "ShellCode": "string",
///  "StatorCode": "string",
///  "RotorCode": "string"
/// }</param>
export const BindShellCode = data => {
  return axios.post(`api/MachineData/BindShellCode`, data)
}

// 'http://localhost:9001/api/MachineData/UploadData'
// {
//     "Code": "string",
//     "Order": 0,
//     "Name": "string",
//     "Result": "string",
//     "Description": "string",
//     "Content": "string",
//     "Time": "2023-03-21T04:50:48.164Z"
//   }
export const UploadData = data => {
  return axios.post(`api/MachineData/UploadData`, data)
}

// 'http://localhost:9001/api/MachineData/GetData?Start=2023-03-21%2000%3A00&End=2023-03-22%2000%3A00'

// {
//   Start: '2023-03-22',
//     End: '2023-03-24',
//   page: 1,
//   size: 10
// }
export const GetData = (params) => {
  return axios.get('api/MachineData/GetData', {
    params
  })
}
// {
//   Start: '2023-03-22',
//     End: '2023-03-24',
//   page: 1,
//   size: 10
// }
export const GetProductData = (params) => {
  return axios.get('api/MachineData/GetProductData', {
    params
  })
}

/// curl -X 'GET' \
//   'http://localhost:9001/api/MachineData/GetBindingData/1/50' \
//   -H 'accept: text/plain' \
//   -H 'request-from: swagger'
export const GetBindingData = (page, size) => {
  return axios.get(`api/MachineData/GetBindingData/${page}/${size}`)
}
// curl -X 'GET' \
//   'http://127.0.0.1:9001/api/MachineData/GetProductDataByName?Start=2021-01-01&End=2025-01-01&Name=%E5%AE%9A%E5%AD%901&page=1&size=20' \
//   -H 'accept: text/plain' \
//   -H 'request-from: swagger'

export const GetProductDataByName = (params) => {
  return axios.get('api/MachineData/GetProductDataByName', {
    params
  })
}

// curl -X 'GET' \
//   'http://localhost:9001/api/MachineData/GetUploadDataNames?Start=2023-05-01&End=2023-07-01' \
//   -H 'accept: text/plain'

export const GetUploadDataNames = (params) => {
  return axios.get('api/MachineData/GetUploadDataNames', {
    params
  })
}

export default {
  GetData,
  GetProductData,
  UploadData,
  BindShellCode,
  GetMachineDataByCode,
  GetBindingData,
  GetUploadDataNames,
  GetProductDataByName }
