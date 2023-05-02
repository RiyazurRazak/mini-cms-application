import axios from 'axios'
import { baseUrl } from '../constants/constants'

export const getPage = (slug) => axios.get(`${baseUrl}/pages/page/${slug}`)
