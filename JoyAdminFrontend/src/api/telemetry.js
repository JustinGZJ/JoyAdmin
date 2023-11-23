import { axios } from '@/libs/api.request'
export const TelemetryDataDynamicFilter = (params) => {
  return axios.post('api/TelemetryData/DynamicFilter', params)
}
export default {
  TelemetryDataDynamicFilter
}