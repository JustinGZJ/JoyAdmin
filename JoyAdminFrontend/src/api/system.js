import { axios } from '@/libs/api.request'

export const SearchTenant = form => {
  return axios.post('api/System/SearchTenant', form)
}

export const SaveTenant = form => {
  return axios.post('api/System/SaveTenant', form)
}

export const DeleteTenant = id => {
  return axios.delete('api/System/DeleteTenant/' + id)
}
