import axios from 'axios'
import { baseUrl } from '../constants/constants'

export const getActiveTheme = () => axios.get(`${baseUrl}/meta/theme`)

export const getMetaDetails = () => axios.get(`${baseUrl}/meta`)
