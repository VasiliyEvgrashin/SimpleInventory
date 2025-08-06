export interface AppNavigation {
  title: string,
  routeLink: string,
  icon?: string,
  children?: AppNavigation[],
  childrenOpened?: boolean
}
